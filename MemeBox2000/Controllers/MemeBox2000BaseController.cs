using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Socha3.Common.DataAccess.EF.Domo.Models;
using Socha3.MemeBox2000.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Socha3.MemeBox2000.Controllers
{
    public abstract class MemeBox2000BaseController : Controller
    {
        public IMemoryCache cache { get; set; }

        public static DomoEntities GetCtx()
        {
            return new DomoEntities();
        }

        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        public long? CurrentUserId()
        {
            using (var ctx = GetCtx())
            {
                var email = ClaimsPrincipal.Current.FindFirst("email")?.Value?.ToLower();
                var dbUser = ctx.Users.FirstOrDefault(u => u.Email.ToLower() == email);
                return dbUser?.UserId;
            }

        }

        public User CurrentUser()
        {
            using (var ctx = GetCtx())
            {
                var email = ClaimsPrincipal.Current.FindFirst("email")?.Value?.ToLower();
                var dbUser = ctx.Users.FirstOrDefault(u => u.Email.ToLower() == email);
                return dbUser;
            }
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(filterContext, next);
            var nameClaim = ClaimsPrincipal.Current.FindFirst("email");

            if (nameClaim != null && !string.IsNullOrEmpty(nameClaim.Value))
                ViewBag.Name = nameClaim.Value;

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUserId = CurrentUserId();
        }

        /// <summary>
        /// Gets all memes from the db and stores to cache while returning the meme info models.  If genreFilter is provided, only memes whose genre starts with the provided text will be returned. Regardless, all memes will be be in the cache after this call no matter what.
        /// </summary>
        /// <param name="genreFilter"></param>
        /// <returns></returns>
        public List<MemeInfo> GetAllMemesFromCache(string genreFilter = null)
        {
            using (var ctx = GetCtx())
            {
                var allMemes = new List<MemeInfo>();
                var ids = ctx.Memes.Select(m => m.MemeId);
                ids.ToList().ForEach(id =>
                {
                    var memeInfo = GetMemeFromCache(id);
                    if (memeInfo != null)
                        allMemes.Add(memeInfo);
                });

                if (genreFilter != null)
                {
                    genreFilter = genreFilter.Trim().ToLower();
                    allMemes = allMemes.Where(m => m.Genre.Trim().ToLower().StartsWith(genreFilter)).ToList();
                }

                return allMemes;
            }
        }

        public static MemeInfo MapMeme(Meme meme)
        {
            return new MemeInfo
            {
                Description = meme.Description,
                Title = meme.Title,
                Genre = meme.Genre,
                Id = meme.MemeId,
                UserId = meme.User?.UserId,
                UserEmail = meme.User?.Email,
                UserFirstName = meme.User?.FirstName,
                UserLastName = meme.User?.LastName,
                Image = meme.File,
                MimeType = meme.MimeType
            };
        }

        /// <summary>
        /// Attempts to get the MemeInfo from cache. If not found, will attempt to refresh from database and add to cache.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MemeInfo GetMemeFromCache(long id)
        {
            var memeInfo = cache.GetOrCreate<MemeInfo>($"meme{id}", cache =>
            {
                using (var ctx = GetCtx())
                {
                    var meme = ctx.Memes.FirstOrDefault(m => m.MemeId == id);

                    if (meme != null)
                    {
                        var memeInfosd = MapMeme(meme);
                        return memeInfosd;
                    }
                    else
                        return null;
                }
            });

            return memeInfo;
        }

        /// <summary>
        /// Deletes the memeInfo from cache which will force a refresh from the db if it still exists
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMemeFromCache(long id)
        {
            cache.Remove($"meme{id}");
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}