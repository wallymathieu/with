using System;
using Xunit;
using With;
using With.Rubyfy;
using With.Collections;
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
    }
}

