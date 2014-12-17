using Ploeh.AutoFixture.Xunit;
using System;
using Xunit;
using Xunit.Extensions;
using With.Destructure;

namespace Tests.Deestructure
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
    }
}
