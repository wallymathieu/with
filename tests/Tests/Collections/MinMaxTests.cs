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
        public void By()
        {
            Assert.Equal("c", _sampleCollection1.MaxBy(T.GetI).a);
            Assert.Equal("c", _sampleCollection2.MinBy(T.GetI).a);
        }
        [Fact]
        public void Min() => 
            Assert.Equal("a", _sampleCollection1.Min(T.Compare).a);
        [Fact]
        public void Max() => 
            Assert.Equal("c", _sampleCollection1.Max(T.Compare).a);
    }
}

