using System;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
using With;
namespace Tests
{
	public class BuildingAs
	{
		public class MyClass
		{
			public MyClass(int myProperty, string myProperty2)
			{
				MyProperty = myProperty;
				MyProperty2 = myProperty2;
			}
			public int MyProperty { get; private set; }
			public string MyProperty2 { get; private set; }
		}

		public class MyClass2 : MyClass
		{
			public DateTime MyProperty3 { get; set; }
			public MyClass2(int myProperty, string myProperty2, DateTime myProperty3)
				: base(myProperty, myProperty2)
			{
				MyProperty3 = myProperty3;
			}
		}

		[Test]
		public void A_class_should_map_its_parents_properties_and_get_the_new_value()
		{
			var time = new DateTime(2001, 1, 1);
			MyClass2 ret = new MyClass(1, "2").As<MyClass2>()
				.Eql(p=>p.MyProperty3,time);
			Assert.Equal(ret.MyProperty, 1);
			Assert.Equal(ret.MyProperty2, "2");
			Assert.Equal(ret.MyProperty3, time);
		}
	}
}

