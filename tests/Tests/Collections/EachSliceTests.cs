using System.Linq;
using AutoFixture.Xunit2;
using Tests.Json;
using Xunit;

namespace Tests.Collections
{
    public class EachSliceTests
    {
        [Fact]
        public void Test() => Assert.Equal(new[]{
            new []{1, 2, 3},
            new []{4, 5, 6},
            new []{7, 8, 9},
            new []{10}
        }.ToJson(), Enumerable.Range(1,10).EachSlice(3).ToJson());

        [Theory, AutoData]
        public void Int_range(int size) => 
            Assert.Equal(size, Enumerable.Range(0, size * 2 - 1).EachSlice(2).Count());

        [Theory, AutoData]
        public void Array(int size) => 
            Assert.Equal(size, new object[size * 2].EachSlice(2).Count());
    }
}

