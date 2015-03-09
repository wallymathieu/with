using System;
using With.Rubyfy;
using Xunit;
using With;
using With.Rubyfy.Json;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;


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

        [Theory, AutoData]
        public void Int_range(int size)
        {
            Assert.Equal(size, 0.To(size*2-1).EachSlice(2).Count());
        }

        [Theory, AutoData]
        public void Array(int size)
        {
            Assert.Equal(size, new object[size*2].EachSlice(2).Count());
        }
    }
}

