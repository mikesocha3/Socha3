using System.Collections.Generic;

namespace Socha3.Common.Extensions
{
    public static class IDictionaryExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IDictionary<TKey, TValue> iDict)
        {
            return new Dictionary<TKey, TValue>(iDict);
        }
    }
}
