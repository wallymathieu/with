using System;
using Xunit;
using With.Rubyfy;

namespace Tests
{
    public class MinMaxTests
    {
        [Fact]
        public void StringCompare(){
            Assert.Equal("c", new []{"a","b","c"}.Max());
            Assert.Equal("a", new []{"a","b","c"}.Min());
        }
        [Fact]
        public void MaxBy(){
            Assert.Equal("c", new []{new {a="a",i=10},new {a="b",i=15},new {a="c",i=20}}.MaxBy(e=>e.i).First().a);
            Assert.Equal("c", new []{new {a="a",i=20},new {a="b",i=15},new {a="c",i=10}}.MinBy(e=>e.i).First().a);
        }

    }
}

