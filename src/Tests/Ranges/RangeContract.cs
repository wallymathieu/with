using Xunit;
using Xunit.Extensions;
using Tests.Ranges.Adapters;

namespace Tests.Ranges
{
    public class RangeContract
    {
        [Theory, RangesData]
        public void New_range(IRangeConverter c)
        {
            Assert.Equal (c.Range(0,2).ToArray(),
                c.ToArrayOf(new []{ 0, 1, 2 }));
        }

        [Theory, RangesData]
        public void New_negative_range(IRangeConverter c)
        {
            Assert.Equal (c.Range (0,-2).ToArray(),
                c.ToArrayOf(new []{ 0, -1, -2 }));
        }

        [Theory, RangesData]
        public void Range_with_step(IRangeConverter c)
        {
            Assert.Equal(c.Range(0,4,2).ToArray(), 
                c.ToArrayOf(new[] { 0, 2, 4 }));
        }

        [Theory, RangesData]
        public void Range_have(IRangeConverter c)
        {
            var range =c.Range(0,4,2);
            Assert.Equal (range.Contain(2), true);
            Assert.Equal(range.Contain(0), true);
            Assert.Equal(range.Contain(4), true);
        }

        [Theory, RangesData]
        public void Negative_range_have(IRangeConverter c)
        {
            var range = c.Range(0,-4,2);
            Assert.Equal (range.Contain(-2), true);
            Assert.Equal(range.Contain(0), true);
            Assert.Equal(range.Contain(-4), true);
        }

        [Theory, RangesData]
        public void Range_doesn_not_have(IRangeConverter c)
        {
            var range = c.Range (0,4,2);
            Assert.Equal(range.Contain(-1), false);
            Assert.Equal(range.Contain(1), false);
            Assert.Equal(range.Contain(5), false);
        }

        [Theory, RangesData]
        public void Negative_range_does_not_have(IRangeConverter c)
        {
            var range = c.Range (0,-4,2);
            Assert.Equal (range.Contain(1), false);
            Assert.Equal(range.Contain(4), false);
            Assert.Equal (range.Contain(-1), false);
            Assert.Equal (range.Contain(-3), false);
        }
    }
	
}
