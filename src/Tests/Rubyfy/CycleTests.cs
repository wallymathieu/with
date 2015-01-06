using System;
using With.Rubyfy;
using Xunit;

namespace Tests
{
    public class CycleTests
    {
        [Fact]
        public void Example1()
        {
            var array = new []{"a", "b", "c"};

            Assert.Equal(new []{
                "a", "b", "c"
            }, array.Cycle(1).ToA());
            Assert.Equal(new []{
                "a", "b", "c",
                "a", "b", "c"
            }, array.Cycle(2).ToA());
        }

        [Fact]
        public void Example2()
        {
            var array = new []{"a", "b", "c"};

            Assert.Equal(new []{
                "a", "b", "c",
                "a", "b", "c",
                "a"
            }, array.Cycle(null).Take(7).ToA());
        }

    }
}

