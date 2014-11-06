using System;
using With;
using NUnit.Framework;
namespace Tests
{	
	[TestFixture]
	public class MatchFuncTests
	{
		private string DoMatch(int v){
			return Switch.Match<int,string> (v)
				.Case (1, () => "One!")
				.Case (new []{ 2, 3, 5, 7, 11 }, p => "This is a prime!")
				.Case (13.To (19), t => "A teen")
				.Case (i=>i==42,(i)=>"Meaning of life")
				.Case (i=>i==52,()=>"Some other number")
				.Else (_ => "Ain't special");
		}

		[Test]
		public void Test_one(){
			Assert.That (DoMatch (1), Is.EqualTo ("One!"));
		}
		[Test]
		public void Test_prime(){
			Assert.That (DoMatch (7), Is.EqualTo ("This is a prime!"));
		}
		[Test]
		public void Test_teen(){
			Assert.That (DoMatch (17), Is.EqualTo ("A teen"));
		}
		[Test]
		public void Test_other(){
			Assert.That (DoMatch (200), Is.EqualTo ("Ain't special"));
			Assert.That (DoMatch (29), Is.EqualTo ("Ain't special"));
		}
		[Test]
		public void Test_meaning_of_life(){
			Assert.That (DoMatch (42), Is.EqualTo ("Meaning of life"));
		}
        [Test]
        public void Test_does_not_match()
        {
            Assert.Throws<NoMatchFoundException>(() => {
                string one = Switch.Match<int, string>(2)
                    .Case(1, () => "One!");
            });
        }

        [Test]
        public void Regex_find_first()
        {
            var instance = "m";

            int result = Switch.Match<string, int>(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Regex_find_complicated()
        {
            var instance = "Rio1";

            int result = Switch.Match<string, int>(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Regex_use_match_object()
        {
            var instance = "Rio3";

            int result = Switch.Match<string, int>(instance)
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}(\\d{1,})", m => Int32.Parse(m.Groups[1].Value));
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Regex_find_complicated_differnt_order()
        {
            var instance = "Rio1";

            int result = Switch.Match<string, int>(instance)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3)
                .Case("m", m => 1)
                .Case("s", m => 2);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Regex_prepared_Find_complicated()
        {
            var instance = "Rio1";

            var result = Switch.Match<string, int>()
                .Case("m", m => 1)
                .Case("s", m => 2)
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", m => 3);
            Assert.That(result.ValueOf(instance), Is.EqualTo(3));
        }

	}
}

