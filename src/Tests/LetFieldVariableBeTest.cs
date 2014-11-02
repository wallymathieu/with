using System;
using NUnit.Framework;
using With;

namespace Tests
{
	[TestFixture]
	public class LetFieldVariableBeTest
	{
		class MyClass
		{
			public int Value = 100;
			public static int StaticField = 1000;
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
			using (Let.Of(()=> MyClass.StaticField)
					  .Be(13))
			{
				Assert.That(MyClass.StaticField, Is.EqualTo(13));
			}
			Assert.That(MyClass.StaticField, Is.EqualTo(1000));
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

