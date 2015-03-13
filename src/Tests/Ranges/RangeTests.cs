using System;
using With;
using Xunit;
using System.Linq;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Tests.Ranges
{
	public class RangeTests
	{
		[Fact]
		public void New_Int_range()
		{
			Assert.Equal (new Range<int> (0,2).ToArray(),new []{ 0, 1, 2 });
		}
        [Fact]
        public void New_negative_Int_range()
        {
            Assert.Equal (new Range<int> (0,-2).ToArray(),new []{ 0, -1, -2 });
        }

        [Theory, AutoData]
		public void Int_range(int size)
		{
            Assert.Equal(0.To(size).ToArray(), new Range<int>(0, size).ToArray());
		}
		[Fact]
		public void Int_range_with_step()
		{
            Assert.Equal(0.To(4).Step(2).ToArray(), new[] { 0, 2, 4 });
		}
		[Fact]
		public void Int_range_have()
		{
			var range = 0.To(4).Step (2);
			Assert.Equal (range.Contain(2), true);
            Assert.Equal(range.Contain(0), true);
            Assert.Equal(range.Contain(4), true);
		}
        [Fact]
        public void Negative_int_range_have()
        {
            var range = 0.To(-4).Step(2);
            Assert.Equal (range.Contain(-2), true);
            Assert.Equal(range.Contain(0), true);
            Assert.Equal(range.Contain(-4), true);
        }
        [Fact]
        public void Int_range_doesn_not_have()
        {
            var range = 0.To(4).Step(2);
            Assert.Equal(range.Contain(-1), false);
            Assert.Equal(range.Contain(1), false);
            Assert.Equal(range.Contain(5), false);
        }
        [Fact]
        public void Negative_int_range_does_not_have()
        {
            var range = 0.To(-4).Step(2);
            Assert.Equal (range.Contain(1), false);
            Assert.Equal(range.Contain(4), false);
            Assert.Equal (range.Contain(-1), false);
            Assert.Equal (range.Contain(-3), false);
        }

		[Fact]
		public void New_Long_range()
		{
            Assert.Equal(new Range<long>(0, 2).ToArray(), new long[] { 0, 1, 2 });
		}
		[Fact]
		public void Long_range()
		{
            Assert.Equal(0L.To(2L).ToArray(), new[] { 0L, 1L, 2L });
		}
		[Fact]
		public void Long_range_with_step()
		{
            Assert.Equal(0L.To(4L).Step(2).ToArray(), new long[] { 0L, 2L, 4L });
		}

		[Fact]
		public void Long_range_has()
		{
			var range = 0L.To (4L).Step (2L);
            Assert.Equal(range.Contain(2), true);
            Assert.Equal(range.Contain(0), true);
            Assert.Equal(range.Contain(4), true);

            Assert.Equal(range.Contain(-1), false);
            Assert.Equal(range.Contain(1), false);
            Assert.Equal(range.Contain(5), false);
        }
		[Fact]
		public void New_Decimal_range()
		{
            Assert.Equal(new Range<Decimal>(0.1m, 2.1m).ToArray(), new[] { 0.1m, 1.1m, 2.1m });
		}
		[Fact]
		public void Decimal_range()
		{
            Assert.Equal(0.1m.To(2.1m).ToArray(), new[] { 0.1m, 1.1m, 2.1m });
		}
		[Fact]
		public void Decimal_range_with_step()
		{
            Assert.Equal(0.1m.To(4.1m).Step(2).ToArray(), new[] { 0.1m, 2.1m, 4.1m });
		}
		[Fact]
		public void Decimal_range_has()
		{
			var range = 0.1m.To(4.1m).Step(2m);
			Assert.Equal(range.Contain(2.1m), true);
			Assert.Equal(range.Contain(0.1m), true);
			Assert.Equal(range.Contain(4.1m), true);

			Assert.Equal(range.Contain(-1.1m), false);
			Assert.Equal(range.Contain(1.1m), false);
			Assert.Equal(range.Contain(5.1m), false);

			Assert.Equal(range.Contain(2m), false);
			Assert.Equal(range.Contain(0m), false);
			Assert.Equal(range.Contain(4m), false);
		}
	}
}

