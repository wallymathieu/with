using Xunit;
using With;
using With.Rubyfy;
using System.Collections.Generic;
using With.ReadonlyEnumerable;

namespace Tests
{
    public class IEnumerable_Add
    {
        [Fact]
        public void Can_add_range()
        {
            var range = 0.To(3).ToL().Tap(l => l.AddRange((-1).To(-3)));
            Assert.Equal(new[] { 0, 1, 2, 3, -1, -2, -3 }, range.ToA());
        }

        [Fact]
        public void Add_wont_modify()
        {
            var l = (IEnumerable<int>)0.To(3).ToL();
            var range = l.ToL().Tap(ll => ll.Add(0));
            Assert.Equal(new[] { 0, 1, 2, 3 }, l.ToA());
        }

        [Fact]
        public void Add_range_wont_modify()
        {
            var l = (IEnumerable<int>)0.To(3).ToL();
            var range = l.ToL().Tap(ll => ll.AddRange((-1).To(-3)));
            Assert.Equal(new[] { 0, 1, 2, 3 }, l.ToA());
        }
    }
}

