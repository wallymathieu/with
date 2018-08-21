using System.Linq;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class FlatMapTests
    {
        [Fact]
        public void SimpleReturn()
        {
            var array = new[] { 1, 2, 3, 4 };
            Assert.Equal(new[] { 1, -1, 2, -2, 3, -3, 4, -4 }, array.Select(e => new[] { e, -e }).ToArray().Flatten());
        }
    }
}

