using System;
using With;
using Ploeh.AutoFixture;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Tests
{
	public class LetFieldVariableBeTest
	{
		public class MyClass
		{
			public int Value;
            public readonly int ValueReadonly = 100;
			public static int StaticField=9356;
		}

        [Theory,AutoData]
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
			using (Let.Member(()=> MyClass.StaticField)
					  .Be(temporary))
			{
                Assert.Equal(MyClass.StaticField, temporary);
			}
            Assert.NotEqual(MyClass.StaticField, temporary);
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
        public void Test_instance_set_readonly(MyClass myClass, int temporary)
        {
            using (Let.Member(() => myClass.ValueReadonly)
                      .Be(temporary))
            {
                Assert.Equal(myClass.ValueReadonly, temporary);
            }
            Assert.NotEqual(myClass.ValueReadonly, temporary);
        }

	}
}

