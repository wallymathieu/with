using System;
using With;
using NUnit.Framework;
namespace Tests
{	
	[TestFixture,Ignore("Not implemented")]
	public class MatchFuncTests
	{
		private string DoMatch(int v){
			return Switch.Match<int,string> (v)
				.Case (1, () => "One!")
				.Case (new []{ 2, 3, 5, 7, 11 }, p => "This is a prime!")
				.Case (13.To (19), t => "A teen")
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
			Assert.That (DoMatch (200), Is.EqualTo ("A teen"));
		}
	}
}

