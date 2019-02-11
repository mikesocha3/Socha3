using System.Collections;
using System.Collections.Generic;

namespace Socha3.Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns whether this object is equal to any of the provided arguments.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tests"></param>
        /// <returns></returns>
        public static bool In(this object input, params object[] tests)
        {
            bool @in = InArray(input, tests);
            return @in;
        }

        /// <summary>
        /// Returns whether this object is equal to any of the items in the provided IList
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tests"></param>
        /// <returns></returns>
        public static bool In<T>(this T input, IEnumerable<T> tests)
        {
            bool @in = InArray(input, tests);
            return @in;
        }

        private static bool InArray<T>(object input, IEnumerable<T> tests)
        {
            foreach (var test in tests)
            {
                if (input.Equals(test))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Casts the actual object to type T, as opposed to the IEnumerable extension Cast&lt;T&gt; which casts each item in the collection to type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object obj)
        {
            return (T)obj;
        }
    }
}
