using Ploeh.AutoFixture.Xunit;
using With;
using With.Destructure;
using Xunit;
using Xunit.Extensions;

namespace Tests.Destructure
{
	public class DestructureClassTests
    {
        public class MyClass0
        {
            public MyClass0(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly string B;
            public readonly string C;
            public string Af { get { return A; } }
            public string Bf { get { return B; } }
            public string Cf { get { return C; } }
        }

        public class MyClass1
        {
            public MyClass1(string a, int b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly int B;
            public readonly string C;
            public string Af { get { return A; } }
            public int Bf { get { return B; } }
            public string Cf { get { return C; } }
        }

        public class MyClass2
        {
            public MyClass2(string a, int b, int c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly int B;
            public readonly int C;
            public string Af { get { return A; } }
            public int Bf { get { return B; } }
            public int Cf { get { return C; } }
        }

        private static int MatchFields(object instance)
        {
            return Switch.Match<object, int>(instance).Fields(
               f => f
                    .Case((string a) => -1)
                    .Case((string a, string b, string c) => 1)
                    .Case((string a, int b) => 20)
                    .Case((string a, int b, string c, string d) => 50)
                    .Case((string a, int b, string c) => 2)
                    .Case((string a, string b, int c) => 3)
                    .Case((string a, int b, int c) => 4)
                  );
        }
        private static int MatchProperties(object instance)
        {
            return Switch.Match<object, int>(instance).Properties(
               f => f
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
            MyClass0 instance)
        {
            Assert.Equal(1, MatchFields(instance));
            Assert.Equal(1, MatchProperties(instance));
        }

        [Theory, AutoData]
        public void Should_match_the_second_case(
            MyClass1 instance)
        {
            Assert.Equal(2, MatchFields(instance));
            Assert.Equal(2, MatchProperties(instance));
        }

        [Theory, AutoData]
        public void Should_match_the_forth_case(
            MyClass2 instance)
        {
            Assert.Equal(4, MatchFields(instance));
            Assert.Equal(4, MatchProperties(instance));
        }
    }
}
