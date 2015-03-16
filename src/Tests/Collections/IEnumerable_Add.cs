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
        public void Can_insert()
        {
            var range = 0.To(3).Insert(0, -1);
            Assert.Equal(new []{ -1, 0, 1, 2, 3}, range.ToA());
        }

        [Fact]
        public void Can_insert_range()
        {
            var range = 0.To(3).InsertRange(0, (-1).To(-3));
            Assert.Equal(new []{ -1,-2,-3, 0, 1, 2, 3 }, range.ToA());
        }

        [Fact]
        public void Insert_wont_modify()
        {
            var l = (IEnumerable<int>)0.To(3).ToL();
            var range = l.Insert(0, -1);
            Assert.Equal(new []{ 0, 1, 2, 3 }, l.ToA());
        }

        [Fact]
        public void Insert_range_wont_modify()
        {
            var l = (IEnumerable<int>)0.To(3).ToL();
            var range = l.InsertRange(0,(-1).To(-3));
            Assert.Equal(new []{ 0, 1, 2, 3 }, l.ToA());
        }
    }
}

