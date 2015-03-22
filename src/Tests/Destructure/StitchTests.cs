using Ploeh.AutoFixture.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using With;
using With.Destructure;
using Xunit;
using Xunit.Extensions;

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
    }
}
