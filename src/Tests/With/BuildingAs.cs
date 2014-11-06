using System;
using NUnit.Framework;
using With;
namespace Tests
{
    [TestFixture, Category("With")]
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
			Assert.That(ret.MyProperty, Is.EqualTo(1));
			Assert.That(ret.MyProperty2, Is.EqualTo("2"));
			Assert.That(ret.MyProperty3, Is.EqualTo(time));
		}
	}
}

