using System;
using Xunit;
using With.Rubyfy;
using With;
using System.Collections.Generic;


namespace Tests
{
    public class GrepTests
    {
        [Fact]
        public void GrepString(){
            var array = new []{ "test", "lorem", "ipsum", "seek", "seek_start", "lorem", "ipsum", "seek_end"};
            Assert.Equal(new []{ "seek", "seek_start", "seek_end" }, array.Grep("seek").ToA());
        }
        [Fact]
        public void GrepOther(){
            Assert.Equal(38.To(44).ToA(),
                1.To(100).Grep(38.To(44)).ToA());
        }
        [Fact]
        public void GrepHash(){
            Assert.Equal<int[]>(38.To(44).ToA(),
                1.To(100).Grep(38.To(44).ToH(i=>i, i=>i)).ToA());
        }
        [Fact]
        public void GrepSet(){
            Assert.Equal<int[]>(38.To(44).ToA(),
                1.To(100).Grep(new HashSet<int>(38.To(44))).ToA());
        }
    }
}

