using System;
using System.Linq;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class PairwiseTests
    {
        [Fact]
        public void Pairwise()
        {
            var r = Enumerable.Range(0, 5);
            var result = r.Pairwise(Tuple.Create);
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 4) },
                result.ToArray());
        }
        [Fact]
        public void Pairwise_tuple()
        {
            var result = Enumerable.Range(0, 5).Pairwise((i, i1) => (i,i1)); //TODO: Add overload next API version
            Assert.Equal(new[] { (0, 1), (1, 2), (2, 3), (3, 4) },
                result.ToArray());
        }

        [Fact]
        public void Pairwise_for_list_of_odd_length()
        {
            var result = Enumerable.Range(0, 4).Pairwise(Tuple.Create);
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) },
                result.ToArray());
        }
        [Fact]
        public void Pairwise_for_list_of_odd_length_tuple()
        {
            var result = Enumerable.Range(0, 4).Pairwise((i, i1) => (i,i1)); //TODO: Add overload next API version
            Assert.Equal(new[] { (0, 1), (1, 2), (2, 3) },
                result.ToArray());
        }
        [Fact]
        public void ShouldHandleEmptyCollection()
        {
            var emptyList = new string[0];
            var result = emptyList.Pairwise(Tuple.Create).ToList();
            Assert.Empty(result);
        }
        [Fact]
        public void ShouldHandleEmptyCollection_tuple()
        {
            var emptyList = new string[0];
            var result = emptyList.Pairwise((i, i1) => (i,i1)).ToList(); //TODO: Add overload next API version
            Assert.Empty(result);
        }
    }
}
