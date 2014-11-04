using System;
using With;
using NUnit.Framework;
namespace Tests
{	
	[TestFixture,Ignore("Not implemented")]
	public class MatchActionTests
	{
		private string DoMatch(int v){
			string retval = null;
			Switch.Match<int> (v)
				.Case (1, () => retval="One!")
				.Case (new []{ 2, 3, 5, 7, 11 }, p => retval="This is a prime!")
				.Case (13.To (19), t => retval="A teen")
				.Else (_ => retval="Ain't special");
			return retval;
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
			Assert.That (DoMatch (200), Is.EqualTo ("A teen"));
		}
	}
}
