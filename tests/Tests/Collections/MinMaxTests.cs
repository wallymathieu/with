using System.Collections.Generic;
using System.Linq;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class MinMaxTests
    {
        private readonly T[] _sampleCollection1 = { new T{ a = "a", i = 10 }, new T{ a = "b", i = 15 }, new T{ a = "c", i = 20 } };
        private readonly T[] _sampleCollection2 = { new T{ a = "a", i = 20 }, new T{ a = "b", i = 15 }, new T{ a = "c", i = 10 } };
        class T
        {
            public string a { get; set; }
            public int i { get; set; }
            public static int GetI(T t) => t.i;
            public static int Compare(T t1, T t2) => t1.i.CompareTo(t2.i);
        }
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
            Assert.Equal("c", _sampleCollection1.MaxBy(T.GetI).a);
            Assert.Equal("c", _sampleCollection2.MinBy(T.GetI).a);
        }
        [Fact]
        public void MinMaxBy()
        {
            var result = _sampleCollection1.MinMaxBy(T.GetI);
            // scientific terms:
            Assert.Equal("c", result.Maxima.First().a);
            Assert.Equal("a", result.Minima.First().a);
            // normal terms:
            Assert.Equal("c", result.Maximums.First().a);
            Assert.Equal("a", result.Minimums.First().a);
        }
        [Fact]
        public void Minimums() => 
            Assert.Equal("a", _sampleCollection1.Minimums(T.Compare).First().a);
        [Fact]
        public void MinimumsBy() => 
            Assert.Equal("a", _sampleCollection1.MinimumsBy(T.GetI).First().a);
        [Fact]
        public void Minima() => 
            Assert.Equal("a", _sampleCollection1.Minima(T.Compare).First().a);
        [Fact]
        public void MinimaBy() => 
            Assert.Equal("a", _sampleCollection1.MinimaBy(T.GetI).First().a);
        [Fact]
        public void Min() => 
            Assert.Equal("a", _sampleCollection1.Min(T.Compare).a);

        [Fact]
        public void Maximums() => 
            Assert.Equal("c", _sampleCollection1.Maximums(T.Compare).First().a);
        [Fact]
        public void MaximumsBy() => 
            Assert.Equal("c", _sampleCollection1.MaximumsBy(T.GetI).First().a);
        [Fact]
        public void Max() => 
            Assert.Equal("c", _sampleCollection1.Max(T.Compare).a);
        [Fact]
        public void Maxima() => 
            Assert.Equal("c", _sampleCollection1.Maxima(T.Compare).First().a);
        [Fact]
        public void MaximaBy() => 
            Assert.Equal("c", _sampleCollection1.MaximaBy(T.GetI).First().a);


        [Fact]
        public void MinMax()
        {
            var result = _sampleCollection1.MinMax(T.Compare);
            Assert.Equal("c", result.Maxima.First().a);
            Assert.Equal("a", result.Minima.First().a);
        }
    }
}

