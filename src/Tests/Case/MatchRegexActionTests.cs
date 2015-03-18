using With;
using Xunit;
namespace Tests
{
	public class MatchRegexActionTests
	{
		private string DoMatch(string v){
			string retval = null;
			Switch.Match<string> (v)
				.Case ("1", () => retval= "One!")
                .Regex("[A-Z]{1}[a-z]{2}\\d{1,}", p => { retval = "Happ!"; })
				.Case (i=>i=="42",(i)=>retval="Meaning of life")
				.Case (i=>i=="52",()=>retval="Some other number")
				.Else (_ => retval="Ain't special");
			return retval;
		}

		[Fact]
		public void Test_one(){
			Assert.Equal(DoMatch ("1"), "One!");
		}
		[Fact]
		public void Test_complicated(){
			Assert.Equal(DoMatch ("Rio1"), "Happ!");
		}

		[Fact]
		public void Test_other(){
			Assert.Equal(DoMatch ("200"), "Ain't special");
			Assert.Equal(DoMatch ("29"), "Ain't special");
		}
		[Fact]
		public void Test_meaning_of_life(){
			Assert.Equal(DoMatch ("42"), "Meaning of life");
		}

	}
}
