using System;
using Xunit;
using With;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
namespace Tests
{
    public class Another_instance_gets_created_without_the_need_for_inheritance
    {
        public class MyClass2 
        {
            private readonly int myProperty;
            private readonly string myProperty2;
            private readonly DateTime myProperty3;
            public MyClass2(int myProperty, string myProperty2, DateTime time)
            {
                this.myProperty = myProperty;
                this.myProperty2 = myProperty2;
                this.myProperty3 = time;
            }
            public int MyProperty { get { return myProperty; } private set { throw new Exception(); } }
            public string MyProperty2 { get { return myProperty2; } private set { throw new Exception(); } }
            public DateTime MyProperty3 { get { return myProperty3; } private set { throw new Exception(); } }
        }

        [Theory, AutoData]
        public void A_class_should_map_properties(MyClass myClass, DateTime time)
        {
            var ret = myClass.As<MyClass2>(time);
            Assert.Equal(ret.MyProperty, myClass.MyProperty);
            Assert.Equal(ret.MyProperty2, myClass.MyProperty2);
            Assert.Equal(ret.MyProperty3, time);
        }
    }
}
