using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socha3.Common.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts to string using Encoding.UTF8
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ReadToString(this byte[] bytes)
        {
            var result = Encoding.UTF8.GetString(bytes);
            return result;
        }
    }
}
