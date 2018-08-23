using With;
using Xunit;

namespace Tests
{
    public class EnumsTests
    {
        enum MyEnum
        {
            A = 0,
            B = 1,
            C = 2
        }

        [Fact]
        public void GetName() => Assert.Equal("C", Enums.GetName(MyEnum.C));
        [Fact]
        public void Parse() => Assert.Equal(MyEnum.C, Enums.Parse<MyEnum>("C"));
        [Fact]
        public void Parse_Ignore_case() => Assert.Equal(MyEnum.C, Enums.Parse<MyEnum>("c", true));
    }
}
