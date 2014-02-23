using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var result = Switch.Regex(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result.Value(), Is.EqualTo(1));
        }

        [Test]
        public void Find_complicated()
        {
            var instance = "Rio1";

            var result = Switch.Regex(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Case("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result.Value(), Is.EqualTo(3));
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
