using System;
using Xunit;
using With.Linq;
using With;
using System.Collections.Generic;

namespace Tests
{
    public class ToHTests
    {
        [Fact]
        public void TupleToHash()
        {
            Assert.Equal(new Dictionary<int, string> { { 1, "one" }, { 2, "two" } },
                new[] { Tuple.Create(1, "one"), Tuple.Create(2, "two") }.ToDictionary());
        }
        [Fact]
        public void KvToHash()
        {
            Assert.Equal(new Dictionary<int, string> { { 1, "one" }, { 2, "two" } },
                new[] { new KeyValuePair<int, string>(1, "one"), new KeyValuePair<int, string>(2, "two") }.ToDictionary());
        }
        [Fact]
        public void ArrayToHash()
        {
            Assert.Equal(new Dictionary<object, object> { { 1, "one" }, { 2, "two" } },
                new[] { new object[] { 1, "one" }, new object[] { 2, "two" } }.ToDictionary());
        }
        [Fact]
        public void ArrayToHashFails()
        {
            Assert.Throws<WrongArrayLengthException>(() =>
                new[] { new object[] { 1, "one" }, new object[] { 2 } }.ToDictionary());
        }
    }
}

