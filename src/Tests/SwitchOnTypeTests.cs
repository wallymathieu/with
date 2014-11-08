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

            Assert.Equal(result, 1);
        }

        [Fact]
        public void Multi_case()
        {
            var instance = new MyClass();

            int result = Switch.On(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Equal(result, 1);
        }

		[Fact]
		public void Multi_case_3()
		{
			var instance = new MyClass3();

			int result = Switch.On(instance)
				.Case((MyClass c) => 1)
				.Case((MyClass2 c) => 2)
				.Case((MyClass3 c) => 3);
			Assert.Equal(result, 3);
		}

        [Fact]
        public void Multi_case_with_a_differnt_order()
        {
            var instance = new MyClass();

            var result = Switch.On(instance)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Case((MyClass c) => 1);
			Assert.Equal(result.Value(), 1);
        }

        [Fact]
        public void Prepared_Multi_case()
        {
            var instance = new MyClass();

            var result = Switch.On()
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.Equal(result.ValueOf(instance), 1);
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
    }
}
