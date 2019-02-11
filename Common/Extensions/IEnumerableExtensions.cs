using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Socha3.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<T> ToList<T>(this IEnumerable enumer)
        {
            var list = new List<T>();
            foreach (var i in enumer)
            {
                list.Add((T)i);
            }
            return list;
        }

        public static string Join<T>(this IEnumerable<T> input, string separator)
        {
            string joined;

            var array = input.ToArray();

            joined = string.Join(separator, array);

            return joined;
        }

        public static object First(this IEnumerable enumer)
        {
            var i = enumer.GetEnumerator();
            i.MoveNext();
            return i.Current;
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            enumeration.ToList().ForEach(item => action(item));
        }

        /// <summary>
        /// Invokes "action" on all but the 1st item in the enumeration.  "firstAction" is invoked only on the first item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFirstAction"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        /// <param name="firstAction"></param>
        public static void ForEachFirst<T, TFirstAction>(this IEnumerable<T> enumeration, Action<T> action, Action<T> firstAction)
        {
            bool firstDone = false;
            foreach (T item in enumeration)
            {
                if (firstDone)
                    action(item);
                else
                {
                    firstDone = true;
                    firstAction(item);
                }
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
