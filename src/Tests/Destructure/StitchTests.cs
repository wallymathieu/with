using System;
using System.Collections.Generic;
using System.Linq;
using With;
using With.Destructure;
using Xunit;

namespace Tests.Destructure
{
    public class StitchTests
    {
        [Fact]
        public void Stitch_without_return_value()
        {
            var r = 0.To(4);
            var result = new List<Tuple<int, int>>();
            r.Stitch((i, j) => {
                result.Add(Tuple.Create(i, j));
            });
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 4) },
                result.ToArray());
        }

        [Fact]
        public void Stitch_with_return_value()
        {
            var r = 0.To(4);
            var result = r.Stitch((i, j) => Tuple.Create(i, j));
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 4) },
                result.ToArray());
        }

        [Fact]
        public void Stitch_for_list_of_odd_length()
        {
            var r = 0.To(3);
            var result = r.Stitch((i, j) => Tuple.Create(i, j));
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) },
                result.ToArray());
        }

        [Fact]
        public void Stitch_with_end_of()
        {
            var r = 0.To(2);
            var result = r.Stitch((i, j) => Tuple.Create(i, (int?)j), i => Tuple.Create(i, (int?)null));
            Assert.Equal(new[] { Tuple.Create(0, (int?)1), Tuple.Create(1, (int?)2), Tuple.Create(2, (int?)null) },
                result.ToArray());
        }

        [Fact]
        public void Stitch_with_end_of_odd_length()
        {
            var r = 0.To(3);
            var result = r.Stitch((i, j) => Tuple.Create(i, (int?)j), i=>Tuple.Create(i, (int?)null));
            Assert.Equal(new[] { Tuple.Create(0, (int?)1), Tuple.Create(1, (int?)2), Tuple.Create(2, (int?)3) , Tuple.Create(3, (int?)null) },
                result.ToArray());
        }

    }
}
