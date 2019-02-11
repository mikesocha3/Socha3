using System.Web;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using  Socha3.MemeBox2000.Models;
using System.Xml.Serialization;
using Socha3.Common.DataAccess.EF.Domo.Models;

namespace  Socha3.MemeBox2000
{
    public static class Util
    {
        /// <summary>
        /// The current path the servers app data folder.
        /// </summary>
        public static string UploadsPath
        {
            get { return HttpContext.Current.ApplicationInstance.Server.MapPath("~/Content/uploads"); }
        }

        private static string _memeDatabasePath;
        public static string MemeDatabasePath
        {
            get
            {
                if(_memeDatabasePath == null)
                    _memeDatabasePath = Path.Combine(Util.UploadsPath, "MemeDatabase.xml");

                return _memeDatabasePath;
            }
        }

        /// <summary>
        /// Takes a filename such as photo.jpg and returns photo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TruncateExtension(string input)
        {
            if (input != null && input.Contains("."))
                return input.Substring(0, input.LastIndexOf('.'));
            else
                return input;
        }

        /// <summary>
        /// Takes a filename such as photo.jpg and returns jpg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetExtension(string input)
        {
            if (input != null && input.Contains("."))
                return input.Substring(input.LastIndexOf('.')+1, input.Length - input.LastIndexOf('.')-1);
            else
                return input;
        }        

        public static bool IsEveryFourth(int test)
        {
            return test % 4 == 0;
        }
    }
}