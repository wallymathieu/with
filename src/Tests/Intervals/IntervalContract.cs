using Xunit;
using Xunit.Extensions;
using Tests.Intervals.Adapters;
namespace Tests.Intervals
{
    public class IntervalContract
    {
        [Theory, IntervalData]
        public void Interval_have(IIntervalConverter c)
        {
            var interval = c.Interval(0, 4);
            Assert.Equal(interval.Contains(c.ToVal(2)), true);
            Assert.Equal(interval.Contains(c.ToVal(0)), true);
            Assert.Equal(interval.Contains(c.ToVal(4)), true);
        }

        [Theory, IntervalData]
        public void Negative_interval_have(IIntervalConverter c)
        {
            var interval = c.Interval(0, -4);
            Assert.Equal(interval.Contains(c.ToVal(-2)), true);
            Assert.Equal(interval.Contains(c.ToVal(0)), true);
            Assert.Equal(interval.Contains(c.ToVal(-4)), true);
        }

        [Theory, IntervalData]
        public void Interval_doesn_not_have(IIntervalConverter c)
        {
            var interval = c.Interval(0, 4);
            Assert.Equal(interval.Contains(c.ToVal(-1)), false);
            Assert.Equal(interval.Contains(c.ToVal(5)), false);
        }

        [Theory, IntervalData]
        public void Negative_interval_does_not_have(IIntervalConverter c)
        {
            var interval = c.Interval(0, -4);
            Assert.Equal(interval.Contains(c.ToVal(1)), false);
            Assert.Equal(interval.Contains(c.ToVal(4)), false);
        }
    }

}
