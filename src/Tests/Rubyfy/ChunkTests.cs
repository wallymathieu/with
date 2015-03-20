using Xunit;
using With.Rubyfy;
using System.Collections.Generic;
namespace Tests.Rubyfy
{
    public class ChunkTests
    {
        [Fact]
        public void Example1()
        {
            var array = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var chunked = new List<object[]>();

            array.Chunk(n => n.Even()).Each((even, ary) => chunked.Add(new object[] { even, ary.ToA() }));
            Assert.Equal(new[]{
                new object[]{false, new []{3, 1}},
                new object[]{true, new []{4}},
                new object[]{false, new []{1, 5, 9}},
                new object[]{true, new []{2, 6}},
                new object[]{false, new []{5, 3, 5}}
                }, chunked.ToA());
        }

        private bool? Drop9And6(int i)
        {
            return i == 9 || i == 6 ? (bool?)null : i.Even();
        }

        [Fact]
        public void ShouldDropItemsWhenNullIsReturned()
        {
            var array = new[] { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var chunked = new List<object[]>();

            array.Chunk(Drop9And6).Each((even, ary) => chunked.Add(new object[] { even, ary.ToA() }));
            Assert.Equal(new[]{
                new object[]{false, new []{3, 1}},
                new object[]{true, new []{4}},
                new object[]{false, new []{1, 5}},
                new object[]{true, new []{2}},
                new object[]{false, new []{5, 3, 5}}
            }, chunked.ToA());
        }

    }
}
