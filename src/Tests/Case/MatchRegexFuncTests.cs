using With;
using Xunit;
namespace Tests
{
    public class MatchRegexFuncTests
    {
        private string DoMatch(string v)
        {
            return Switch.Match<string, string>(v)
                .Case("1", () => "One!")
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", p => "Happ!")
                .Case(i => i == "42", (i) => "Meaning of life")
                .Case(i => i == "52", () => "Some other number")
                .Else(_ => "Ain't special");
        }

        [Fact]
        public void Test_one()
        {
            Assert.Equal(DoMatch("1"), "One!");
        }
        [Fact]
        public void Test_complicated()
        {
            Assert.Equal(DoMatch("Rio1"), "Happ!");
        }

        [Fact]
        public void Test_other()
        {
            Assert.Equal(DoMatch("200"), "Ain't special");
            Assert.Equal(DoMatch("29"), "Ain't special");
        }
        [Fact]
        public void Test_meaning_of_life()
        {
            Assert.Equal(DoMatch("42"), "Meaning of life");
        }

    }

}
