using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Socha3.MemeBox2000.Models;
using System.Web.Mvc;
using Socha3.Common.DataAccess.EF.Domo.Models;
using System.Net;
using System;
using Socha3.Common.Tools;

namespace Socha3.MemeBox2000.Controllers
{
    public class GalleryController : MemeBox2000BaseController
    {
        [HttpGet]
        public ActionResult Index(string genreFilter = null)
        {
            var allMemes = GetAllMemesFromCache(genreFilter);
            return View(allMemes);
        }

        [HttpGet]
        public ActionResult Detail(long id)
        {
            var memeInfo = GetMemeFromCache(id);
            if(memeInfo != null)
                return View(memeInfo);
            else
                throw new Exception($"Meme detail id: {{{id}}} not found.");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(MemeInfo model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = GetCtx())
                {
                    var meme = ctx.Memes.FirstOrDefault(m => m.MemeId == model.Id);
                    if (meme != null)
                    {
                        meme.Description = model.Description;
                        meme.Genre = model.Genre;
                        meme.Title = model.Title;
                        ctx.SaveChanges();

                        ViewBag.SuccessMessage = $"Meme \"{meme.Title}\" updated!";

                        DeleteMemeFromCache(model.Id);
                    }
                }
            }

            return View("Detail", model);
        }

        [Authorize]
        public ActionResult Delete(long id)
        {
            using (var ctx = GetCtx())
            {
                var meme = ctx.Memes.FirstOrDefault(m => m.MemeId == id);
                if (meme != null)
                {
                    ctx.Memes.Remove(meme);
                    ctx.SaveChanges();
                }

                DeleteMemeFromCache(id);

                return View("Index", GetAllMemesFromCache());
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Submit(MemeInfo model)
        {
            var file = Request.Files["file"];
            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("file", "Upload File is requred.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);
            else
            {
                using (var ctx = GetCtx())
                {
                    ctx.Memes.Add(new Meme
                    {
                        MimeType = MimeMapping.GetMimeMapping(file.FileName),
                        File = ReadFully(file.InputStream),
                        Description = model.Description,
                        Genre = model.Genre,
                        Title = model.Title,
                        UserId = CurrentUserId()
                    });
                    ctx.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }

        /// <summary>
        /// Returns image or thumbnail content for the meme
        /// </summary>
        /// <param name="id"></param>
        /// <param name="longestSide"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Meme(long id, int? longestSide = null)
        {
            var memeInfo = GetMemeFromCache(id);

            if (memeInfo != null)
            {
                if (longestSide.HasValue)
                    return File(memeInfo.GetThumbnail(longestSide.Value).Bytes, memeInfo.MimeType);
                else
                    return File(memeInfo.Image, memeInfo.MimeType);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public ActionResult GenreMatches(string genre)
        {
            genre = genre.Trim().ToLower();
            var allMemes = GetAllMemesFromCache(genre);
            var genres = allMemes.Select(m => m.Genre.Trim().ToLower()).Distinct().OrderBy(g => g).ToList();
            return Json(genres, JsonRequestBehavior.AllowGet);
        }
    }
}

