using System;
using System.Linq;
using With;
using With.Linq;
using Xunit;

namespace Tests.Destructure
{
    public class PairwiseTests
    {
        [Fact]
        public void Pairwise()
        {
            var r = 0.To(4);
            var result = r.Pairwise((i, j) => Tuple.Create(i, j));
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 4) },
                result.ToArray());
        }

        [Fact]
        public void Pairwise_for_list_of_odd_length()
        {
            var r = 0.To(3);
            var result = r.Pairwise((i, j) => Tuple.Create(i, j));
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) },
                result.ToArray());
        }
    }
}
