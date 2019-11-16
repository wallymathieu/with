using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using With.Collections;

namespace With.RegularExpressions
{
    /// <summary>
    /// 
    /// </summary>
    public static class GrepExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TRet> Grep<TRet>(this IEnumerable<string> self, Regex regex, Func<string, TRet> map)
        {
            return self.Grep(regex).Select(map);
        }
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> Grep(this IEnumerable<string> self, Regex regex)
        {
            return self.Where(s => s.Match(regex).Success);
        }
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TRet> Grep<TRet>(this IEnumerable<string> self, string regex, Func<string, TRet> map)
        {
            return self.Grep(regex).Select(map);
        }
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> Grep(this IEnumerable<string> self, string regex)
        {
            return self.Grep(regex.AsRegex());
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TRet> Grep<T, TRet>(this IEnumerable<T> self, IContainer<T> pattern, Func<T, TRet> map)
        {
            return self.Grep(pattern).Select(map);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<T> Grep<T>(this IEnumerable<T> self, IContainer<T> pattern)
        {
            return self.Where(s => pattern.Contains(s));
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TRet> Grep<T, TValue, TRet>(this IEnumerable<T> self, IDictionary<T, TValue> pattern, Func<T, TRet> map)
        {
            return self.Grep(pattern).Select(map);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<T> Grep<T, TValue>(this IEnumerable<T> self, IDictionary<T, TValue> pattern)
        {
            return self.Where(s => pattern.ContainsKey(s));
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TRet> Grep<T, TRet>(this IEnumerable<T> self, ISet<T> pattern, Func<T, TRet> map)
        {
            return self.Grep(pattern).Select(map);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<T> Grep<T>(this IEnumerable<T> self, ISet<T> pattern)
        {
            return self.Where(s => pattern.Contains(s));
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<TRet> Grep<T, TRet>(this IEnumerable<T> self, ICollection<T> pattern, Func<T, TRet> map)
        {
            return self.Grep(pattern).Select(map);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<T> Grep<T>(this IEnumerable<T> self, ICollection<T> pattern)
        {
            return self.Where(s => pattern.Contains(s));
        }

    }
}

