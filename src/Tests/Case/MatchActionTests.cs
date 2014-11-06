using System;
using With;
using NUnit.Framework;
namespace Tests
{	
	[TestFixture, Category("Case")]
	public class MatchActionTests
	{
		private string DoMatch(int v){
			string retval = null;
			Switch.Match<int> (v)
				.Case (1, () => retval="One!")
				.Case (new []{ 2, 3, 5, 7, 11 }, p => retval="This is a prime!")
				.Case (13.To (19), t => retval="A teen")
				.Case (i=>i==42,(i)=>retval="Meaning of life")
				.Case (i=>i==52,()=>retval="Some other number")
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
			Assert.That (DoMatch (200), Is.EqualTo ("Ain't special"));
			Assert.That (DoMatch (29), Is.EqualTo ("Ain't special"));
		}
		[Test]
		public void Test_meaning_of_life(){
			Assert.That (DoMatch (42), Is.EqualTo ("Meaning of life"));
		}
	}
}
