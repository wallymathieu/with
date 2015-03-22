using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;
using With.Destructure;
using With;
using With.Rubyfy;

namespace Tests.Destructure
{
    public class DestructureListsTests
    {
        [Theory, AutoData]
        public void First_variable(
            int a, int b, int c)
        {
            Assert.Equal(a, new[] { a, b, c }.Let((x, _) => x));

            int result = -1;
            new[] { a, b, c }.Let((x, _) => result = x);
            Assert.Equal(a, result);
        }

        [Theory, AutoData]
        public void Second_variable(
            int a, int b, int c)
        {
            Assert.Equal(b, new[] { a, b, c }.Let((x, y, _) => y));

            int result = -1;
            new[] { a, b, c }.Let((x, y, _) => result = y);
            Assert.Equal(b, result);
        }

        [Fact]
        public void Yield_range()
        {
            var range = 0.To(4);
            var yielded = range.Stitch((a, b) => new[] { a, b }).ToA();
            Assert.Equal(new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 } }, yielded);
        }
    }
}
