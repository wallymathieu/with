using System;
using With;
using Xunit;

namespace Tests
{
    public class UrisTests
    {
        [Fact]
        public void Create() => Assert.Equal(new Uri("http://some.se/arb/eq"), Uris.Create("http://some.se/","arb/eq"));
        [Fact]
        public void Create2() => Assert.Equal(new Uri("http://some.se/arb/eq"), Uris.Create("http://some.se/a","/arb/eq"));
    }
}
