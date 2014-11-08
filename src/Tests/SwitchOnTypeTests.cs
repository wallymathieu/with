using Xunit;
using With;

namespace Tests
{
    public class SwitchOnTypeTests
    {
        public class MyClass{}

        public class MyClass2{}

        public class MyClass3{}

        [Fact]
        public void Single_case()
        {
            var instance = new MyClass();

            int result = Switch.On(instance)
                .Case((MyClass c) => 1);

            Assert.Equal(1, result);
        }

        [Fact]
        public void Multi_case()
        {
            var instance = new MyClass();

            int result = Switch.On(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Equal(1, result);
        }

		[Fact]
		public void Multi_case_3()
		{
			var instance = new MyClass3();

			int result = Switch.On(instance)
				.Case((MyClass c) => 1)
				.Case((MyClass2 c) => 2)
				.Case((MyClass3 c) => 3);
			Assert.Equal(3, result);
		}

        [Fact]
        public void Multi_case_else()
        {
            var instance = new object();

            int result = Switch.On(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Else(_=>4);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Multi_case_with_a_differnt_order()
        {
            var instance = new MyClass();

            var result = Switch.On(instance)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Case((MyClass c) => 1);
			Assert.Equal(1, result.Value());
        }

        [Fact]
        public void Prepared_Multi_case()
        {
            var instance = new MyClass();

            var result = Switch.On()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Equal(1, result.ValueOf(instance));
        }

        [Fact]
        public void Should_throw_when_fails_to_match()
        {
            var result = Switch.On(new object())
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Throws<NoMatchFoundException>(()=>result.Value());
        }
        [Fact]
        public void Should_throw_when_fails_to_match_prepared()
        {
            var instance = new MyClass();

            var result = Switch.On()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Throws<NoMatchFoundException>(() => result.ValueOf(new object()));
        }
        [Fact]
        public void Should_exec_else_when_fails_to_match_prepared()
        {
            var instance = new MyClass();

            var result = Switch.On()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Else(_=>4);
            Assert.Equal(4, result.ValueOf(new object()));
        }
    }
}
