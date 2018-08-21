using System;
using System.Collections.Generic;
using With.Collections;
using Xunit;

namespace Tests.Collections
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
    }
}

