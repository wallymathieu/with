using System;
using With;
using NUnit.Framework;
namespace Tests
{
    [TestFixture, Category("Case")]
	public class MatchRegexFuncTests
	{
		private string DoMatch(string v){
			return Switch.Match<string,string> (v)
				.Case ("1", () => "One!")
				.Regex ("[A-Z]{1}[a-z]{2}\\d{1,}", p => "Happ!")
				.Case (i=>i=="42",(i)=>"Meaning of life")
				.Case (i=>i=="52",()=>"Some other number")
				.Else (_ => "Ain't special");
		}

		[Test]
		public void Test_one(){
			Assert.That (DoMatch ("1"), Is.EqualTo ("One!"));
		}
		[Test]
		public void Test_complicated(){
			Assert.That (DoMatch ("Rio1"), Is.EqualTo ("Happ!"));
		}

		[Test]
		public void Test_other(){
			Assert.That (DoMatch ("200"), Is.EqualTo ("Ain't special"));
			Assert.That (DoMatch ("29"), Is.EqualTo ("Ain't special"));
		}
		[Test]
		public void Test_meaning_of_life(){
			Assert.That (DoMatch ("42"), Is.EqualTo ("Meaning of life"));
		}

	}

}
