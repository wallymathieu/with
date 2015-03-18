using Xunit;
using Xunit.Extensions;
using System.Linq;
using Ploeh.AutoFixture.Xunit;
using With;
using With.ReadonlyEnumerable;
using System.Collections.Generic;

namespace Tests.With
{
	public class Manipulation_of_enumerable
	{
		[Theory, AutoData]
		public void Should_be_able_to_add_to_enumerable(
	MyClass myClass, string newValue)
		{
			var ret = myClass.With(m => m.MyProperty3.Add(newValue));
			Assert.Equal(newValue, ret.MyProperty3.Last());
		}

        [Theory, AutoData]
        public void Should_be_able_to_union_add_to_enumerable(
            MyClass myClass, string newValue)
        {
            var ret = myClass.With(m => m.MyProperty3.Union(new []{newValue}));
            Assert.Equal(newValue, ret.MyProperty3.Last());

            var array = new []{ newValue };
            ret = myClass.With(m => m.MyProperty3.Union(array));
            Assert.Equal(newValue, ret.MyProperty3.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_concat_add_to_enumerable(
            MyClass myClass, string newValue)
        {
            var ret = myClass.With(m => m.MyProperty3.Concat(new []{newValue}));
            Assert.Equal(newValue, ret.MyProperty3.Last());

            var array = new []{ newValue };
            ret = myClass.With(m => m.MyProperty3.Concat(array));
            Assert.Equal(newValue, ret.MyProperty3.Last());
        }

		[Theory, AutoData]
		public void Should_be_able_to_add_const_to_enumerable(
            MyClass myClass)
		{
			const string newValue = "const";
			var ret = myClass.With(m => m.MyProperty3.Add(newValue));
			Assert.Equal(newValue, ret.MyProperty3.Last());

            ret = myClass.With(m => m.MyProperty3.Concat(new []{newValue}));
            Assert.Equal(newValue, ret.MyProperty3.Last());

            var array = new []{ newValue };
            ret = myClass.With(m => m.MyProperty3.Concat(array));
            Assert.Equal(newValue, ret.MyProperty3.Last());
		}

		[Theory, AutoData]
		public void Should_be_able_to_add_range_with_new_array_to_enumerable(
            MyClass myClass, string newValue)
		{
			var ret = myClass.With(m => m.MyProperty3.AddRange(new[] { newValue }));
			Assert.Equal(newValue, ret.MyProperty3.Last());
		}

		[Theory, AutoData]
		public void Should_be_able_to_add_range_to_enumerable(
            MyClass myClass, IEnumerable<string> newValue)
		{
			var ret = myClass.With(m => m.MyProperty3.AddRange(newValue));
			Assert.Equal(newValue.Last(), ret.MyProperty3.Last());
		}

		[Theory, AutoData]
		public void Should_be_able_to_remove_from_enumerable(
            MyClass myClass)
		{
			var first = myClass.MyProperty3.First();
			var ret = myClass.With(m => m.MyProperty3.Remove(first));
			Assert.NotEqual(first, ret.MyProperty3.First());
		}

        [Theory, AutoData]
        public void Should_be_able_to_where_remove_from_enumerable(
            MyClass myClass)
        {
            var first = myClass.MyProperty3.First();
            var ret = myClass.With(m => m.MyProperty3.Where(p=>p!=first));
            Assert.NotEqual(first, ret.MyProperty3.First());
        }
	}
}
