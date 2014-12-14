using System;
using With;
using Xunit;
using With.General;
using System.Text.RegularExpressions;

namespace Tests
{
    public class MatchFuncTests
    {
        private string DoMatch(int v)
        {
            return Switch.Match<int, string>(v)
                .Case(1, () => "One!")
                .Case(new[] { 2, 3, 5, 7, 11 }, p => "This is a prime!")
                .Case(13.To(19), t => "A teen")
                .Case(i => i == 42, (i) => "Meaning of life")
                .Case(i => i == 52, () => "Some other number")
                .Else(_ => "Ain't special");
        }

        [Fact]
        public void Test_one_using_different_syntax()
        {
            string result = Switch.Match<int, string>(1,
                1.AsArray(), _ => "One!",
                new[] { 2, 3, 5, 7, 11 }, _ => "This is a prime!",
                13.To(19), _ => "A teen")
                .Else(_ => "Ain't special");
            Assert.Equal("One!", result);
        }

        [Fact]
        public void Test_one()
        {
            Assert.Equal(DoMatch(1), "One!");
        }
        [Fact]
        public void Test_prime()
        {
            Assert.Equal(DoMatch(7), "This is a prime!");
        }
        [Fact]
        public void Test_teen()
        {
            Assert.Equal(DoMatch(17), "A teen");
        }
        [Fact]
        public void Test_other()
        {
            Assert.Equal(DoMatch(200), "Ain't special");
            Assert.Equal(DoMatch(29), "Ain't special");
        }
        [Fact]
        public void Test_meaning_of_life()
        {
            Assert.Equal(DoMatch(42), "Meaning of life");
        }
        [Fact]
        public void Test_does_not_match()
        {
            Assert.Throws<NoMatchFoundException>(() =>
            {
                string one = Switch.Match<int, string>(2)
                    .Case(1, () => "One!");
            });
        }

        [Fact]
        public void Regex_find_first()
        {
            var instance = "m";

            int result = Switch.Match<string, int>(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.Equal(result, 1);
        }

        [Fact]
        public void Regex_find_complicated()
        {
            var instance = "Rio1";

            int result = Switch.Match<string, int>(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.Equal(result, 3);
        }

        [Fact]
        public void Regex_use_match_object()
        {
            var instance = "Rio3";

            int result = Switch.Match<string, int>(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}(\\d{1,})", m => Int32.Parse(m.Groups[1].Value));
            Assert.Equal(result, 3);
        }

        [Fact]
        public void Regex_find_complicated_differnt_order()
        {
            var instance = "Rio1";

            int result = Switch.Match<string, int>(instance)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3)
                .Case("m", m => 1)
                .Case("s", m => 2);
            Assert.Equal(result, 3);
        }

        [Fact]
        public void Regex_prepared_Find_complicated()
        {
            var instance = "Rio1";

            var result = Switch.Match<string, int>()
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[a-z]{1}[a-z]{2}\\d{1,}", RegexOptions.IgnoreCase, m => 3);
            Assert.Equal(result.ValueOf(instance), 3);
        }

    }
}

