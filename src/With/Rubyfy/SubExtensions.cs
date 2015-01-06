using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace With.Rubyfy
{
    public static class SubExtensions
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

    }
}

