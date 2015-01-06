using System;
using With.Rubyfy;
using Xunit;
using With;
using With.Rubyfy.Json;
namespace Tests
{
    public class EachSliceTests
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(new []{
                new []{1, 2, 3},
                new []{4, 5, 6},
                new []{7, 8, 9},
                new []{10}
            }.ToJson(), 1.To(10).EachSlice(3).ToJson());
        }
    }
}

