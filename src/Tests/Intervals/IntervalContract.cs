using Xunit;
using Xunit.Extensions;
using Tests.Interval.Adapters;
namespace Tests.Intervals
{
    public class IntervalContract
    {
        [Theory, IntervalData]
        public void Interval_have(IIntervalConverter c)
        {
            var range = c.Interval(0, 4);
            Assert.Equal(range.Contains(2), true);
            Assert.Equal(range.Contains(0), true);
            Assert.Equal(range.Contains(4), true);
        }

        [Theory, IntervalData]
        public void Negative_interval_have(IIntervalConverter c)
        {
            var range = c.Interval(0, -4);
            Assert.Equal(range.Contains(-2), true);
            Assert.Equal(range.Contains(0), true);
            Assert.Equal(range.Contains(-4), true);
        }

        [Theory, IntervalData]
        public void Interval_doesn_not_have(IIntervalConverter c)
        {
            var range = c.Interval(0, 4);
            Assert.Equal(range.Contains(-1), false);
            Assert.Equal(range.Contains(5), false);
        }

        [Theory, IntervalData]
        public void Negative_interval_does_not_have(IIntervalConverter c)
        {
            var range = c.Interval(0, -4);
            Assert.Equal(range.Contains(1), false);
            Assert.Equal(range.Contains(4), false);
        }
    }

}
