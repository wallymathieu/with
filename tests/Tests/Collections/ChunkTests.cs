using System.Collections.Generic;
using System.Linq;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class ChunkTests
    {
        [Fact]
        public void Example1()
        {
            var array = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5};
            var chunked = new List<(bool, int[])>();

            array.Chunk(n => n.Even()).Each((even, ary) => chunked.Add((even, ary.ToArray())));
            Assert.Equal(new[]
            {
                (false, new[] {3, 1}),
                (true, new[] {4}),
                (false, new[] {1, 5, 9}),
                (true, new[] {2, 6}),
                (false, new[] {5, 3, 5})
            }, chunked.ToArray());
        }

        private bool? Drop9And6(int i)
        {
            return i == 9 || i == 6 ? (bool?) null : i.Even();
        }

        [Fact]
        public void ShouldDropItemsWhenNullIsReturned()
        {
            var array = new[] {3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5};
            var chunked = new List<(bool?, int[])>();

            array.Chunk(Drop9And6).Each((even, ary) => chunked.Add((even, ary.ToArray())));
            Assert.Equal(new[]
            {
                ((bool?)false, new[] {3, 1}),
                (true, new[] {4}),
                (false, new[] {1, 5}),
                (true, new[] {2}),
                (false, new[] {5, 3, 5})
            }, chunked.ToArray());
        }
    }
}
