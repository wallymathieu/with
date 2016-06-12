using Xunit;
using With;
using Ploeh.AutoFixture.Xunit2;
using System;
namespace Tests
{
    public class SwitchOnTypeTests
    {
        public class MyClass { }

        public class MyClass2 { }

        public class MyClass3 { }

        [Theory, AutoData]
        public void Single_case(
            MyClass instance)
        {
            int result = Switch.Match<object, int>(instance)
                .Case((MyClass c) => 1).Value();

            Assert.Equal(1, result);
        }

        [Theory, AutoData]
        public void Can_use_more_csharpy_looking_syntax(
            MyClass instance)
        {
            int result = Switch.Match<object, int>(instance)
                .Case<MyClass>(c => 1).Value();

            Assert.Equal(1, result);
        }

        [Theory, AutoData]
        public void Should_match_the_first_case(
            MyClass instance)
        {
            int result = Switch.Match<object, int>(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3).Value();
            Assert.Equal(1, result);
        }

        [Theory, AutoData]
        public void Should_match_the_first_case_when_void(
            MyClass instance)
        {
            var result = 0;
            Switch.Match<object>(instance)
                .Case((MyClass c) => {result = 1;})
                .Case((MyClass2 c) => {result = 2;})
                .Case((MyClass3 c) => {result = 3;})
                .ElseFail();
            Assert.Equal(1, result);
        }

        [Theory, AutoData]
        public void Should_match_the_last_case(
            MyClass3 instance)
        {
            int result = Switch.Match<object, int>(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3).Value();
            Assert.Equal(3, result);
        }

        [Theory, AutoData]
        public void Should_match_else_when_an_unknown_type_is_switched_on(
            int instance)
        {
            int result = Switch.Match<object, int>(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Else(_ => 4).Value();
            Assert.Equal(4, result);
        }

        [Theory, AutoData]
        public void Multi_case_with_a_differnt_order(
            MyClass instance)
        {
            var result = Switch.Match<object, int>(instance)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Case((MyClass c) => 1);
            Assert.Equal(1, result.Value());
        }

        [Theory, AutoData]
        public void Prepared_Multi_case(
            MyClass instance)
        {
            var result = Switch.Match<object, int>()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Equal(1, result.ValueOf(instance));
        }

        [Theory, AutoData]
        public void Should_throw_when_fails_to_match(
            object instance)
        {
            var result = Switch.Match<object, int>(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Throws<NoMatchFoundException>(() => result.Value());
        }
        [Theory, AutoData]
        public void Should_throw_when_fails_to_match_prepared(
            int instance)
        {
            var result = Switch.Match<object, int>()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Throws<NoMatchFoundException>(() => result.ValueOf(instance));
        }
        [Theory, AutoData]
        public void Should_exec_else_when_fails_to_match_prepared(
            int instance)
        {
            var result = Switch.Match<object, int>()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Else(_ => 4);
            Assert.Equal(4, result.ValueOf(instance));
        }

    }
}
