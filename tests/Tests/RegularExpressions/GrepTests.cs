using System.Collections.Generic;
using System.Linq;
using With.RegularExpressions;
using Xunit;

namespace Tests.RegularExpressions
{
    public class GrepTests
    {
        [Fact]
        public void GrepString()
        {
            var array = new[] { "test", "lorem", "ipsum", "seek", "seek_start", "lorem", "ipsum", "seek_end" };
            Assert.Equal(new[] { "seek", "seek_start", "seek_end" }, array.Grep("seek").ToArray());
        }
        [Fact]
        public void GrepOther()
        {
            Assert.Equal(Enumerable.Range(38, 44).ToArray(),
                Enumerable.Range(1, 100).Grep(Enumerable.Range(38, 44).ToArray()).ToArray());
        }
        [Fact]
        public void GrepHash()
        {
            Assert.Equal<int[]>(Enumerable.Range(38, 44).ToArray(),
                Enumerable.Range(1, 100).Grep(Enumerable.Range(38, 44).ToDictionary(i => i, i => i)).ToArray());
        }
        [Fact]
        public void GrepSet()
        {
            Assert.Equal<int[]>(Enumerable.Range(38, 44).ToArray(),
                Enumerable.Range(1, 100).Grep(new HashSet<int>(Enumerable.Range(38, 44))).ToArray());
        }
    }
}

