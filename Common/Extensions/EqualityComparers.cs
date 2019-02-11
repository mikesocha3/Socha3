using Socha3.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Socha3.Common.Extensions
{
    public static class EqualityComparers
    {
        /// <summary>
        /// Compare 2 strings after lowercasing.
        /// </summary>
        public static readonly IEqualityComparer<string> CaseSensitiveString = Create<string>((a, b) => a == b);

        /// <summary>
        /// Compare 2 strings after lowercasing.
        /// </summary>
        public static readonly IEqualityComparer<string> CaseInsensitiveString = Create<string>((a, b) => a.ToLower() == b.ToLower());

        /// <summary>
        /// Compare 2 strings after lowercasing and removing their whitespace.
        /// </summary>
        public static readonly IEqualityComparer<string> CaseAndWhitespaceInsensitiveString = Create<string>((a, b) => a.ToLower().RemoveWhitespace() == b.ToLower().RemoveWhitespace());

        /// <summary>
        /// Match 2 strings (after lowercasing removing their whitespace) if string 1 contains string 2 or vice-versa.
        /// </summary>
        public static readonly IEqualityComparer<string> CaseAndWhitespaceInsensitiveContainsString = Create<string>((a, b) =>
        {
            var str1 = a.ToLower().RemoveWhitespace();
            var str2 = b.ToLower().RemoveWhitespace();

            var contains = str1.Contains(str2) || str2.Contains(str1);
            return contains;
        });

        public static IEqualityComparer<T> Create<T>(Func<T, T, bool> equals, Func<T, int> hash = null)
        {
            return new FuncEqualityComparer<T>(equals, hash ?? (t => 1));
        }

        private class FuncEqualityComparer<T> : EqualityComparer<T>
        {
            private readonly Func<T, T, bool> equals;
            private readonly Func<T, int> hash;

            public FuncEqualityComparer(Func<T, T, bool> equals, Func<T, int> hash)
            {
                this.equals = equals;
                this.hash = hash;
            }

            public override bool Equals(T a, T b)
            {
                return a == null
                    ? b == null
                    : b != null && this.equals(a, b);
            }

            public override int GetHashCode(T obj)
            {
                return obj == null ? 0 : hash(obj);
            }
        }
    }
}