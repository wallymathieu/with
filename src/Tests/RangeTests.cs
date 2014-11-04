using System;
using With;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
	[TestFixture]
	public class RangeTests
	{
		[Test]
		public void Int_range()
		{
			var r = new Range<int> (0,2).ToArray();
			Assert.That (r, Is.EquivalentTo (new []{ 0, 1 }));
		}
		[Test]
		public void Long_range()
		{
			var r = new Range<long> (0,2).ToArray();
			Assert.That (r, Is.EquivalentTo (new []{ 0, 1 }));
		}
		[Test]
		public void Float_range()
		{
			var r = new Range<float> (0.1F,2.1F).ToArray();
			Assert.That (r, Is.EquivalentTo (new []{ 0.1F, 1.1F }));
		}
		[Test]
		public void Decimal_range()
		{
			var r = new Range<Decimal> (0.1m,2.1m).ToArray();
			Assert.That (r, Is.EquivalentTo (new []{ 0.1m, 1.1m }));
		}
	}
}

