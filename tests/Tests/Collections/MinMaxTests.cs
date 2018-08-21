using System.Linq;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class MinMaxTests
    {
        [Fact]
        public void StringCompare()
        {
            Assert.Equal("c", new[] { "a", "b", "c" }.Max());
            Assert.Equal("a", new[] { "a", "b", "c" }.Min());
            Assert.Equal(new object[] { "a", "c" }, new[] { "a", "b", "c" }.MinMax().Flatten().Cast<object>().ToArray());
        }

        [Fact]
        public void By()
        {
            Assert.Equal("c", new[] { new { a = "a", i = 10 }, new { a = "b", i = 15 }, new { a = "c", i = 20 } }.MaxBy(e => e.i).a);
            Assert.Equal("c", new[] { new { a = "a", i = 20 }, new { a = "b", i = 15 }, new { a = "c", i = 10 } }.MinBy(e => e.i).a);
        }
        [Fact]
        public void MinMaxBy()
        {
            var result = new[] { new { a = "a", i = 10 }, new { a = "b", i = 15 }, new { a = "c", i = 20 } }.MinMaxBy(e => e.i);
            Assert.Equal("c", result.Maxima.First().a);
            Assert.Equal("a", result.Minima.First().a);
        }
        [Fact]
        public void MinMax()
        {
            var result = new[] { new { a = "a", i = 10 }, new { a = "b", i = 15 }, new { a = "c", i = 20 } }.MinMax((a, b) => a.i.CompareTo(b.i));
            Assert.Equal("c", result.Maxima.First().a);
            Assert.Equal("a", result.Minima.First().a);
        }
    }
}

