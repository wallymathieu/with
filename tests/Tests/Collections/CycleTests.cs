using System.Linq;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class CycleTests
    {
        [Fact]
        public void Example1()
        {
            var array = new[] { "a", "b", "c" };

            Assert.Equal(new[]{
                "a", "b", "c"
            }, array.Cycle(1).ToArray());
            Assert.Equal(new[]{
                "a", "b", "c",
                "a", "b", "c"
            }, array.Cycle(2).ToArray());
        }

        [Fact]
        public void Example2()
        {
            var array = new[] { "a", "b", "c" };

            Assert.Equal(new[]{
                "a", "b", "c",
                "a", "b", "c",
                "a"
            }, array.Cycle(null).Take(7).ToArray());
        }

    }
}

