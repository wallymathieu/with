using System;
using Xunit;
using With;
using With.Rubyfy;
using With.Collections;
using System.Collections.Generic;
namespace Tests
{
    public class IEnumerable_Add
    {
        [Fact]
        public void Can_add()
        {
            var range = 0.To(3).Add(-1);
            Assert.Equal(new []{ 0, 1, 2, 3, -1 }, range.ToA());
        }
        [Fact]
        public void Can_add_range()
        {
            var range = 0.To(3).AddRange((-1).To(-3));
            Assert.Equal(new []{ 0, 1, 2, 3, -1,-2,-3 }, range.ToA());
        }

        [Fact]
        public void Add_wont_modify()
        {
            var l = (IEnumerable<int>)0.To(3).ToL();
            var range = l.Add(0);
            Assert.Equal(new []{ 0, 1, 2, 3 }, l.ToA());
        }

        [Fact]
        public void Add_range_wont_modify()
        {
            var l = (IEnumerable<int>)0.To(3).ToL();
            var range = l.AddRange((-1).To(-3));
            Assert.Equal(new []{ 0, 1, 2, 3 }, l.ToA());
        }
    }
}

