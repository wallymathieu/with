using Xunit;
using With.Ranges;
using With.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Tests
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
            Assert.Equal(38.To(44).ToArray(),
                1.To(100).Grep(38.To(44)).ToArray());
        }
        [Fact]
        public void GrepHash()
        {
            Assert.Equal<int[]>(38.To(44).ToArray(),
                1.To(100).Grep(38.To(44).ToDictionary(i => i, i => i)).ToArray());
        }
        [Fact]
        public void GrepSet()
        {
            Assert.Equal<int[]>(38.To(44).ToArray(),
                1.To(100).Grep(new HashSet<int>(38.To(44))).ToArray());
        }
    }
}

