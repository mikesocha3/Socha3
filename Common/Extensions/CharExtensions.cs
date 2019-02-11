using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socha3.Common.Extensions
{
    public static class CharExtensions
    {
        /// <summary>
        /// Shortcut for char.IsNumber()
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static bool IsNumber(this char chr)
        {
            return char.IsNumber(chr);
        }
    }
}
