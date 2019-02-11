using System;
using System.Collections.Generic;

namespace Socha3.Common.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        /// Executes the given Func for each item in the IList setting the item to the return value of the func. (not available with IEnumerable on for/each loops)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns>this object</returns>
        public static IList<T> ForEachModify<T>(this IList<T> list, Func<T, T> func)
        {
            var listCopy = new List<T>();

            list.ForEach(i => listCopy.Add(func(i))); 
            
            for (int i = 0; i < listCopy.Count; i++)
                list[i] = listCopy[i];

            return list;
        }
    }
}
