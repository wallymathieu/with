using System;
using System.Collections.Generic;
using Xunit;
using With;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Tests
{
	public class A_new_instance_of_a_class_that_inherits_from_the_other
	{
		public class MyClass2 : MyClass
		{
			private readonly DateTime myProperty4;
			public MyClass2(int myProperty, string myProperty2, IEnumerable<string> myProperty3, DateTime myProperty4)
				: base(myProperty, myProperty2, myProperty3)
			{
				this.myProperty4 = myProperty4;
			}

			public DateTime MyProperty4 { get { return myProperty4; } private set { throw new Exception(); } }

		}

		[Theory, AutoData]
		public void A_class_should_map_its_parents_properties(
			MyClass myClass, DateTime time)
		{
			var ret = myClass.As<MyClass2>(time);
			Assert.Equal(time, ret.MyProperty4);

			Assert.Equal(myClass.MyProperty, ret.MyProperty);
			Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
		}

		[Theory, AutoData]
		public void A_class_should_be_able_to_use_lambda(
			MyClass myClass, DateTime time)
		{
			var ret = myClass.As<MyClass2>(m => m.MyProperty4 == time);
			Assert.Equal(ret.MyProperty4, time);

			Assert.Equal(myClass.MyProperty, ret.MyProperty);
			Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
		}

		[Theory, AutoData]
		public void A_class_using_cast(
			MyClass myClass, DateTime time)
		{
			Object _time = (Object)time;
			var ret = myClass.As<MyClass2>(m => m.MyProperty4 == (DateTime)_time);
			Assert.Equal(ret.MyProperty4, time);

			Assert.Equal(myClass.MyProperty, ret.MyProperty);
			Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
		}

		[Theory, AutoData]
		public void A_class_using_const(
	MyClass myClass, DateTime time)
		{
			const string _time = "253654365";
			var ret = myClass.As<MyClass2>(m => m.MyProperty2 == _time && m.MyProperty4 == time);
			Assert.Equal(ret.MyProperty2, _time);
		}

		[Theory, AutoData]
		public void A_class_should_map_its_parents_properties_and_get_the_new_value(
			MyClass myClass, DateTime time)
		{
			MyClass2 ret = myClass.As<MyClass2>()
				.Eql(p => p.MyProperty4, time);
			Assert.Equal(myClass.MyProperty, ret.MyProperty);
			Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
			Assert.Equal(time, ret.MyProperty4);
		}

		public class MyClassWithDifferentOrder : MyClass
		{
			private readonly DateTime myProperty4;
			public MyClassWithDifferentOrder(DateTime myProperty4, int myProperty, IEnumerable<string> myProperty3, string myProperty2)
				: base(myProperty, myProperty2, myProperty3)
			{
				this.myProperty4 = myProperty4;
			}

			public DateTime MyProperty4 { get { return myProperty4; } private set { throw new Exception(); } }
		}

		[Theory, AutoData]
		public void A_class_with_different_order_of_constructor_parameters(
			MyClass myClass, DateTime time)
		{
			MyClassWithDifferentOrder ret = myClass.As<MyClassWithDifferentOrder>()
				.Eql(p => p.MyProperty4, time);
			Assert.Equal(time, ret.MyProperty4);

			Assert.Equal(myClass.MyProperty, ret.MyProperty);
			Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
		}

	}
}
