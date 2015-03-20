using With;
using Xunit;
namespace Tests
{
    public class MatchActionTests
    {
        private string DoMatch(int v)
        {
            string retval = null;
            Switch.Match<int>(v)
                .Case(1, () => retval = "One!")
                .Case(new[] { 2, 3, 5, 7, 11 }, p => retval = "This is a prime!")
                .Case(13.To(19), t => retval = "A teen")
                .Case(i => i == 42, (i) => retval = "Meaning of life")
                .Case(i => i == 52, () => retval = "Some other number")
                .Else(_ => retval = "Ain't special");
            return retval;
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
    }
}
