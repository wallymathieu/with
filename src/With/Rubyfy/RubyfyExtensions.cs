using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace With.Rubyfy
{
    public static class RubyfyExtensions
    {
        private static Regex BackReference = new Regex(@"
(?: # not really needed, just grouping
    (?<slash>\\)? # an optional first slash
    (?<slash_and_digits>\\ # this construct matches \2
        (?<digits>\d+)
    )
)", RegexOptions.IgnorePatternWhitespace);

        private static MatchEvaluator Evaluate(string evaluator)
        {
            return match =>
                       {
                           var refs = GetBackReferences(match);
                           return BackReference.Replace(evaluator, m =>
                           {
                               var digits = m.Groups["digits"];
                               return !m.Groups["slash"].Success && refs.ContainsKey(digits.Value)
                                   ? refs[digits.Value].Value
                                   : m.Groups["slash_and_digits"].Value;
                           });
                       };
        }

        private static Dictionary<string, Group> GetBackReferences(Match match)
        {
            var backReferences = new Dictionary<string, Group>();
            for (int i = 1; i < match.Groups.Count; i++)
            {
                var g = match.Groups[i];
                if (g.Success)
                {
                    backReferences.Add(i.ToString(), g);
                }
            }
            return backReferences;
        }

        private static Regex AsRegex(string regex)
        {
            return FirstSlash.IsMatch(regex)
                ? new Regex(RemoveSlashes(regex), ParseOptions(regex))
                : new Regex(Regex.Escape(regex));
        }

        private static Regex FirstSlash = new Regex("^/", RegexOptions.Multiline);
        private static Regex TrailingSlash = new Regex("/([ixm]*)$", RegexOptions.Multiline);

        private static string RemoveSlashes(string regex)
        {
            return FirstSlash.Replace(regex, "")
                .Yield(s => TrailingSlash.Replace(s, ""));
        }

        private static RegexOptions ParseOptions(string regex)
        {
            var trailingSlash = TrailingSlash.Match(regex).Groups[1];

            if (trailingSlash.Success && trailingSlash.Length > 0)
            {
                return trailingSlash.Value
                    .ToA()
                    .Map(token => ParseOption(token))
                    .Reduce((memo, opt) => memo | opt);
            }
            return RegexOptions.None;
        }

        private static RegexOptions ParseOption(char token)
        {
            switch (token)
            {
                case 'i':
                    return RegexOptions.IgnoreCase;
                case 'x':
                    return RegexOptions.IgnorePatternWhitespace;
                case 'm':
                    return RegexOptions.Singleline | RegexOptions.Multiline;
                default:
                    throw new Exception("Regex option unknown: "+token);
            }
        }

        public static string Gsub(this string self, Regex regex, string evaluator)
        {
            return regex.Replace(self, Evaluate(evaluator));
        }
        public static string Gsub(this string self, string regex, string evaluator)
        {
            return AsRegex(regex).Replace(self ?? String.Empty, Evaluate(evaluator));
        }
        public static string Gsub(this string self, Regex regex, MatchEvaluator evaluator)
        {
            return regex.Replace(self ?? String.Empty, evaluator);
        }

        public static string Sub(this string self, Regex regex, string evaluator)
        {
            return regex.Replace(self ?? String.Empty, Evaluate(evaluator), 1);
        }
        public static string Sub(this string self, string regex, string evaluator)
        {
            return AsRegex(regex).Replace(self ?? String.Empty, Evaluate(evaluator), 1);
        }
        public static string Sub(this string self, Regex regex, MatchEvaluator evaluator)
        {
            return regex.Replace(self ?? String.Empty, evaluator, 1);
        }


        public static Match Match(this string self, Regex regex)
        {
            return regex.Match(self ?? String.Empty);
        }

        public static IEnumerable<TRet> Map<T, TRet>(this IEnumerable<T> self, Func<T, TRet> map)
        {
            return self.Select(map);
        }
        public static IEnumerable<TRet> Collect<T, TRet>(this IEnumerable<T> self, Func<T, TRet> map)
        {
            return self.Select(map);
        }

        public static T Reduce<T>(this IEnumerable<T> self, Func<T, T, T> map)
        {
            return self.Aggregate(map);
        }

        public static T Reduce<T>(this IEnumerable<T> self, T initial, Func<T, T, T> map)
        {
            return self.Aggregate(initial, map);
        }

        /// <summary>
        /// Returns a new array that is a one-dimensional flattening of self (recursively).
        ///
        ///That is, for every element that is an array, extract its elements into the new array.
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable self)
        {
            foreach (var variable in self)
            {
                if (variable is T)
                {
                    yield return (T)variable;
                }
                else
                {
                    foreach (var result in Flatten<T>((IEnumerable)variable))
                    {
                        yield return result;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a new array that is a one-dimensional flattening of self (recursively).
        ///
        ///That is, for every element that is an array, extract its elements into the new array.
        ///
        ///The optional level argument determines the level of recursion to flatten.
        /// </summary>
        public static IEnumerable Flatten(this IEnumerable self, int? order = null)
        {
            if (order == null || order >= 0)
            {
                foreach (var variable in self)
                {
                    if (variable is IEnumerable && !(variable is string))
                    {
                        foreach (var result in Flatten((IEnumerable)variable, order != null ? order - 1 : null))
                        {
                            yield return result;
                        }
                    }
                    else
                    {
                        yield return variable;
                    }
                }
            }
            else
            {
                yield return self;
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            return null == self || !self.Any();
        }

        /// <summary>
        /// split the string into length large pieces
        /// </summary>
        /// <param name="self"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string[] SplitN(this string self, int length)
        {
            var result = new string[self.Length / length];
            var index = 0;
            for (int i = 0; i < self.Length; i += length)
            {
                result[index++] = self.Substring(i, length);
            }
            return result;
        }
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TValue> getvalue)
        {
            if (!self.ContainsKey(key))
            {
                self[key] = getvalue();
            }
            return self[key];
        }
        public static T Detect<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return self.FirstOrDefault(predicate);
        }

        public static TRet MapDetect<T, TRet>(this IEnumerable<T> collection, Func<T, TRet> func) where TRet : class
        {
            foreach (var value in collection)
            {
                TRet result;
                if ((result = func(value)) != null)
                {
                    return result;
                }
            }
            return null;
        }

        public static T[] ToA<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray();
        }

        public static object[] ToA(this IEnumerable enumerable)
        {
            return enumerable.Cast<Object>().ToArray();
        }

        public static IEnumerable<T> SortBy<T, TValue>(this IEnumerable<T> self, Func<T, TValue> sortby)
        {
            return self.OrderBy(sortby);
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> self)
        {
            return self.OrderBy(t => t);
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> self, Func<T, T, int> compare)
        {
            return self.OrderBy(compare);
        }

        public static IEnumerable<T> Select<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return self.Where(predicate);
        }

        public static bool Any<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.Any(self,predicate);
        }

        public static bool All<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.All(self,predicate);
        }
        public static int Count<T>(this IEnumerable<T> self, Func<T,bool> predicate)
        {
            return Enumerable.Count(self,predicate);
        }
        public static int Count<T>(this IEnumerable<T> self)
        {
            return Enumerable.Count(self);
        }
        public static int Count<T>(this IEnumerable<T> self, T value)
        {
            return Enumerable.Count(self, e=> e.Equals(value));
        }
        /*public static IEnumerable<IGrouping<TKey,T>> Chunk<TKey,T>(this IEnumerable<T> self, Func<T,TKey> keySelector)
        {
            return self.GroupBy(keySelector);
        }

[3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5].chunk { |n|
  n.even?
}.each { |even, ary|
  p [even, ary]
}
#=> [false, [3, 1]]
#   [true, [4]]
#   [false, [1, 5, 9]]
#   [true, [2, 6]]
#   [false, [5, 3, 5]]

        */

        /*public static IEnumerable<T> CollectConcat<T>(this IEnumerable<T> self, Func<T, T> map)
        {
            return self.FlatMap(map);
        }
        public static IEnumerable<T> FlatMap<T>(this IEnumerable<T> self, Func<T, T> map)
        {
            return self.Map(map).Flatten<T>();
        }*/

        /*
        Cycle
        */

        /*
        Drop
        */

        /*
        DropWhile
        */
        
        /*
        EachCons
        */

        /*
        each_entry?
        */

        /*EachSlice*/

        /*EachSlice*/

        /*Find == Detect*/

        /*FindAll == Select*/

        /*FindIndex*/

        /*First*/

        /*Grep(array)*/

        /*Include == Member*/

        /*Inject == Reduce*/

        /*Max, MaxBy?*/

        /*Min, MinBy*/

        /*MinMax, MinMaxBy*/

        /*None*/

        /*One*/

        /*Partition*/

        /*Reject*/

        /*SliceAfter*/

        /*SliceBefore*/

        /*SliceWhen*/

        /*Take*/

        /*TakeWhile*/

        /*Zip*/
    }
}
