using System;
using System.Collections.Generic;
using System.Linq;

namespace Comm.Tools.Utility
{
    public static class DistinctHelper
    {
        public class CommonEqualityComparer<T, TV> : IEqualityComparer<T>
        {
            private readonly Func<T, TV> _keySelector;
            private readonly IEqualityComparer<TV> _comparer;

            public CommonEqualityComparer(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }

            public CommonEqualityComparer(Func<T, TV> keySelector)
                : this(keySelector, EqualityComparer<TV>.Default)
            { }

            public bool Equals(T x, T y)
            {
                return _comparer.Equals(_keySelector(x), _keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return _comparer.GetHashCode(_keySelector(obj));
            }
        }

        /// <summary>
        /// 例：a.Distinct(p => p.Name)
        /// </summary>
        public static IEnumerable<T> Distinct<T, TV>(this IEnumerable<T> source, Func<T, TV> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, TV>(keySelector, EqualityComparer<TV>.Default));
        }
        /// <summary>
        /// 例：a.Distinct(p => p.Name, StringComparer.CurrentCultureIgnoreCase)
        /// </summary>
        public static IEnumerable<T> Distinct<T, TV>(this IEnumerable<T> source, Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, TV>(keySelector, comparer));
        }
    }
}
