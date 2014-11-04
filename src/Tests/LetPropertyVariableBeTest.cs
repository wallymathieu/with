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
                ValuePrivateSet = 100;
			}
			public int Value{ get; set;}
            public int ValuePrivateSet { get; private set; }
			private static int _staticValue=1000;
			public static int StaticProperty {
				get{ return _staticValue;}
				set{ _staticValue = value;}
			}
		}

        [Test]
        public void Test_cleaner_syntax()
        {
            var myClass = new MyClass();
            using (myClass.SetTemporary(obj => obj.Value, 13))
            {
                Assert.That(myClass.Value, Is.EqualTo(13));
            }
            Assert.That(myClass.Value, Is.EqualTo(100));
        }

		[Test]
		public void Test()
		{
			var myClass = new MyClass();
			using (Let.Object(myClass)
				.Member(obj=>obj.Value)
				.Be(13))
			{
				Assert.That(myClass.Value, Is.EqualTo(13));
			}
			Assert.That(myClass.Value, Is.EqualTo(100));
		}

		[Test]
		public void Static()
		{
			using (Let.Member(()=> MyClass.StaticProperty)
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
			using (Let.Member(()=>myClass.Value)
				.Be(13))
			{
				Assert.That(myClass.Value, Is.EqualTo(13));
			}
			Assert.That(myClass.Value, Is.EqualTo(100));
		}

		class ClassWithClass
		{
			public MyClass Inner = new MyClass();
		}

		[Test]
		public void Test_instance_on_demeter()
		{
			var myClass = new ClassWithClass();
			using (Let.Member(()=>myClass.Inner.Value)
				.Be(13))
			{
				Assert.That(myClass.Inner.Value, Is.EqualTo(13));
			}
			Assert.That(myClass.Inner.Value, Is.EqualTo(100));
		}

        [Test]
        public void Test_instance_with_private_set()
        {
            var myClass = new MyClass();
            using (Let.Member(() => myClass.ValuePrivateSet)
                .Be(13))
            {
                Assert.That(myClass.ValuePrivateSet, Is.EqualTo(13));
            }
            Assert.That(myClass.ValuePrivateSet, Is.EqualTo(100));
        }
	}
}

