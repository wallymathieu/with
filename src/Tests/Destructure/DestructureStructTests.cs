using Ploeh.AutoFixture.Xunit;
using With;
using With.Destructure;
using Xunit;
using Xunit.Extensions;

namespace Tests.Destructure
{
    public class DestructureStructTests
    {
        public struct MyStruct0
        {
            public MyStruct0(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly string B;
            public readonly string C;
        }

        public struct MyStruct1
        {
            public MyStruct1(string a, int b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly int B;
            public readonly string C;
        }

        public struct MyStruct2
        {
            public MyStruct2(string a, int b, int c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly int B;
            public readonly int C;
        }
        private static int Match(object instance)
        {
            return Switch.Match<object, int>(instance).Fields(
               f => f
                    .IncludeAll()
                    .Case((string a) => -1)
                    .Case((string a, string b, string c) => 1)
                    .Case((string a, int b) => 20)
                    .Case((string a, int b, string c, string d) => 50)
                    .Case((string a, int b, string c) => 2)
                    .Case((string a, string b, int c) => 3)
                    .Case((string a, int b, int c) => 4)
                );
        }

        [Theory, AutoData]
        public void Should_match_the_first_case(
            MyStruct0 instance)
        {
            int result = Match(instance);
            Assert.Equal(1, result);
        }

        [Theory, AutoData]
        public void Should_match_the_second_case(
            MyStruct1 instance)
        {
            int result = Match(instance);
            Assert.Equal(2, result);
        }

        [Theory, AutoData]
        public void Should_match_the_forth_case(
            MyStruct2 instance)
        {
            int result = Match(instance);
            Assert.Equal(4, result);
        }
    }
}
