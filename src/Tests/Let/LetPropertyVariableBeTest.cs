using System;
using With;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Tests
{
	public class LetPropertyVariableBeTest
	{
		public class MyClass
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

        [Theory, AutoData]
        public void Test_cleaner_syntax(MyClass myClass, int temporary)
        {
            using (myClass.SetTemporary(obj => obj.Value, temporary))
            {
                Assert.Equal(myClass.Value, temporary);
            }
            Assert.NotEqual(myClass.Value, temporary);
        }

        [Theory, AutoData]
        public void Test(MyClass myClass, int temporary)
		{
			using (Let.Object(myClass)
				.Member(obj=>obj.Value)
                .Be(temporary))
			{
                Assert.Equal(myClass.Value, temporary);
			}
            Assert.NotEqual(myClass.Value, temporary);
        }

        [Theory, AutoData]
        public void Static(int temporary)
		{
			using (Let.Member(()=> MyClass.StaticProperty)
				.Be(temporary))
			{
                Assert.Equal(MyClass.StaticProperty, temporary);
			}
            Assert.NotEqual(MyClass.StaticProperty, temporary);
        }

        [Theory, AutoData]
        public void Test_instance(MyClass myClass, int temporary)
		{
			using (Let.Member(()=>myClass.Value)
				.Be(temporary))
			{
                Assert.Equal(myClass.Value, temporary);
			}
            Assert.NotEqual(myClass.Value, temporary);
		}

		public class ClassWithClass
		{
			public MyClass Inner = new MyClass();
		}

        [Theory, AutoData]
        public void Test_instance_on_demeter(ClassWithClass myClass, int temporary)
		{
			using (Let.Member(()=>myClass.Inner.Value)
                .Be(temporary))
			{
				Assert.Equal(myClass.Inner.Value, temporary);
			}
            Assert.NotEqual(myClass.Inner.Value, temporary);
		}

        [Theory, AutoData]
        public void Test_instance_with_private_set(MyClass myClass, int temporary)
        {
            using (Let.Member(() => myClass.ValuePrivateSet)
                .Be(temporary))
            {
                Assert.Equal(myClass.ValuePrivateSet, temporary);
            }
            Assert.NotEqual(myClass.ValuePrivateSet, temporary);
        }
	}
}

