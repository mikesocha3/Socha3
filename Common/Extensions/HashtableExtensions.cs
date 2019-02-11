using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Socha3.Common.Extensions
{
    public static class HashtableExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this Hashtable table)
        {
            var dict = table.Cast<DictionaryEntry>()
                .ToDictionary(kvp => (TKey)kvp.Key, kvp => (TValue)kvp.Value);

            return dict;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this Hashtable table, Func<object, TKey> castKey, Func<object, TValue> castValue)
        {
            var dict = table.Cast<DictionaryEntry>()
                .ToDictionary(kvp => castKey(kvp.Key), kvp => castValue(kvp.Value));

            return dict;
        }
    }
}
