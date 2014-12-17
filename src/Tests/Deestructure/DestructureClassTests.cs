using Ploeh.AutoFixture.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using With;
using With.Destructure;
using With.Rubyfy;
using Xunit;
using Xunit.Extensions;

namespace Tests.Deestructure
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
        }
        private static int Match(object instance)
        {
            return Switch.Match<object, int>(instance).Fields(
               f => f
                    .IncludeAll()
                    .Case((string a, string b, string c) => 1)
                    .Case((string a, int b, string c) => 2)
                    .Case((string a, string b, int c) => 3)
                    .Case((string a, int b, int c) => 4)
                );
        }

        [Theory, AutoData]
        public void Should_match_the_first_case(
            MyClass0 instance)
        {
            int result = Match(instance);
            Assert.Equal(1, result);
        }

        [Theory, AutoData]
        public void Should_match_the_second_case(
            MyClass1 instance)
        {
            int result = Match(instance);
            Assert.Equal(2, result);
        }

        [Theory, AutoData]
        public void Should_match_the_forth_case(
            MyClass2 instance)
        {
            int result = Match(instance);
            Assert.Equal(4, result);
        }
    }
}
