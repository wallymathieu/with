using NUnit.Framework;
using With;

namespace Tests
{
    [TestFixture]
    public class RegexSwitchTests
    {
        [Test]
        public void Find_first()
        {
            var instance = "m";

            int result = Switch.Regex(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Find_complicated()
        {
            var instance = "Rio1";

            int result = Switch.Regex(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result, Is.EqualTo(3));
        }

		[Test]
		public void Find_complicated_differnt_order()
		{
			var instance = "Rio1";

			int result = Switch.Regex(instance)
				.Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3)
				.Case("m", m => 1)
				.Case("s", m => 2);
			Assert.That(result, Is.EqualTo(3));
		}

        [Test]
        public void Prepared_Find_complicated()
        {
            var instance = "Rio1";

            var result = Switch.Regex()
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result.ValueOf(instance), Is.EqualTo(3));
        }

    }
}
