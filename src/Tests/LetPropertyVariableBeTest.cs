using System;
using NUnit.Framework;
using With;

namespace Tests
{

	[TestFixture]
	public class LetPropertyVariableBeTest
	{
		class MyClass
		{
			public MyClass ()
			{
				Value = 100;
			}
			public int Value{ get; set;}
			private static int _staticValue=1000;
			public static int StaticProperty {
				get{ return _staticValue;}
				set{ _staticValue = value;}
			}
		}

		[Test]
		public void Test()
		{
			var myClass = new MyClass();
			using (Let.Of(myClass)
				.Property(obj=>obj.Value)
				.Be(13))
			{
				Assert.That(myClass.Value, Is.EqualTo(13));
			}
			Assert.That(myClass.Value, Is.EqualTo(100));
		}

		class ClassWithClass
		{
			public MyClass value = new MyClass();
		}

		[Test]
		public void Static()
		{
			using (Let.Of(()=> MyClass.StaticProperty)
				.Be(13))
			{
				Assert.That(MyClass.StaticProperty, Is.EqualTo(13));
			}
			Assert.That(MyClass.StaticProperty, Is.EqualTo(1000));
		}

		[Test]
		public void Test_instance()
		{
			var myClass = new MyClass();
			using (Let.Of(()=>myClass.Value)
				.Be(13))
			{
				Assert.That(myClass.Value, Is.EqualTo(13));
			}
			Assert.That(myClass.Value, Is.EqualTo(100));
		}
	}
}

