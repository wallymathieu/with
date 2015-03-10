using Ploeh.AutoFixture.Xunit;
using System;
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
        }

        [Theory, AutoData]
        public void Second_variable(
            int a, int b, int c)
        {
            Assert.Equal(b, new[] { a, b, c }.Let((x, y, _) => y));
        }

        [Fact]
        public void Yield_range()
        {
            var range = 0.To(4);
            var yielded = range.EachConsequtivePair( (a,b) => a+b ).ToA();
            Assert.Equal(new[]{1, 3, 5, 7}, yielded);
        }
    }
}
