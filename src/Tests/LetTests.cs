using System;
using NUnit.Framework;
using With;

namespace Tests
{
	[TestFixture]
	public class LetVariableBeTest
	{
		class MyClass
		{
			public int value = 100;
			public static int staticValue = 1000;
		}
	
		[Test]
		public void Test()
		{
			var myClass = new MyClass();
			using (Let.Of(myClass)
					  .Property(obj=>obj.value)
					  .Be(13))
			{
				Assert.That(myClass.value, Is.EqualTo(13));
			}
			Assert.That(myClass.value, Is.EqualTo(100));
		}

		class ClassWithClass
		{
			public MyClass value = new MyClass();
		}

		[Test]
		public void Static()
		{
			using (Let.Of(()=> MyClass.staticValue)
					  .Be(13))
			{
				Assert.That(MyClass.staticValue, Is.EqualTo(13));
			}
			Assert.That(MyClass.staticValue, Is.EqualTo(1000));
		}

		[Test,Ignore("Doesnt work yet")]
		public void Test_instance()
		{
			var myClass = new MyClass();
			using (Let.Of(()=>myClass.value)
					  .Be(13))
			{
				Assert.That(myClass.value, Is.EqualTo(13));
			}
			Assert.That(myClass.value, Is.EqualTo(100));
		}
	}
}

