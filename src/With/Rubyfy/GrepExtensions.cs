using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace With.Rubyfy
{
	public static class GrepExtensions
    {
        public static IEnumerable<TRet> Grep<TRet>(this IEnumerable<string> self, Regex regex, Func<string,TRet> map)
        {
            return self.Grep(regex).Map(map);
        }
        public static IEnumerable<string> Grep(this IEnumerable<string> self, Regex regex)
        {
            return self.Select(s=>s.Match(regex).Success);
        }
        public static IEnumerable<TRet> Grep<TRet>(this IEnumerable<string> self, string regex, Func<string,TRet> map)
        {
            return self.Grep(regex).Map(map);
        }
        public static IEnumerable<string> Grep(this IEnumerable<string> self, string regex)
        {
            return self.Grep(regex.AsRegex());
        }

        public static IEnumerable<TRet> Grep<T,TRet>(this IEnumerable<T> self, IContainer<T> pattern, Func<T,TRet> map)
        {
            return self.Grep(pattern).Map(map);
        }

        public static IEnumerable<T> Grep<T>(this IEnumerable<T> self, IContainer<T> pattern)
        {
            return self.Select(s=>pattern.Contains(s));
        }

        public static IEnumerable<TRet> Grep<T, TValue, TRet>(this IEnumerable<T> self, IDictionary<T,TValue> pattern, Func<T,TRet> map)
        {
            return self.Grep(pattern).Map(map);
        }

        public static IEnumerable<T> Grep<T,TValue>(this IEnumerable<T> self, IDictionary<T,TValue> pattern)
        {
            return self.Select(s=>pattern.ContainsKey(s));
        }

        public static IEnumerable<TRet> Grep<T, TRet>(this IEnumerable<T> self, ISet<T> pattern, Func<T,TRet> map)
        {
            return self.Grep(pattern).Map(map);
        }

        public static IEnumerable<T> Grep<T>(this IEnumerable<T> self, ISet<T> pattern)
        {
            return self.Select(s=>pattern.Contains(s));
        }

        public static IEnumerable<TRet> Grep<T, TRet>(this IEnumerable<T> self, ICollection<T> pattern, Func<T,TRet> map)
        {
            return self.Grep(pattern).Map(map);
        }

        public static IEnumerable<T> Grep<T>(this IEnumerable<T> self, ICollection<T> pattern)
        {
            return self.Select(s=>pattern.Contains(s));
        }

    }
}

