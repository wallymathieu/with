using System;
using Xunit;
using With;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;

namespace Tests
{
    public class WeirdUseTests
    {
        public class MyClass { }

        public class MyClass2 { }

        public class MyClass3 { }

        [Theory, AutoData]
        public void Should_match_be_possible_to_reuse_part_of_the_chain(
            MyClass2 instance)
        {
            var m = Switch.Match<object, int>()
                .Case((MyClass c) => 1);

            var m1 = m
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);

            Assert.Equal(2, m1.ValueOf(instance));

            Assert.Equal(-2, m
                .Case((MyClass2 c) => -2)
                .Case((MyClass3 c) => -3).ValueOf(instance));
            
            Assert.Equal(2, m1.ValueOf(instance));
        }
    }
}

