using System;
using NUnit.Framework;
using With;
namespace Tests
{
	[TestFixture]
	public class BuildingWith
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

		[Test]
		public void A_class_should_be_able_to_create_a_clone_with_a_property_set()
		{
			MyClass ret = new MyClass(1, "2").With()
				.Eql(m => m.MyProperty, 3);
			Assert.That(ret.MyProperty, Is.EqualTo(3));
			Assert.That(ret.MyProperty2, Is.EqualTo("2"));
		}

		[Test]
		public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal()
		{
			MyClass ret = new MyClass(1, "2").With()
				.Eql(m => m.MyProperty, 3)
				.Eql(m => m.MyProperty2, "3");
			Assert.That(ret.MyProperty, Is.EqualTo(3));
			Assert.That(ret.MyProperty2, Is.EqualTo("3"));
		}
	}
}

