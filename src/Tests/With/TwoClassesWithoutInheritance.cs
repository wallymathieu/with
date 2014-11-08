using System;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
using With;
namespace Tests
{
    public class TwoClassesWithoutInheritance
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
        public class MyClass2 
        {
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }
            public DateTime MyProperty3 { get; set; }
            public MyClass2(int myProperty, string myProperty2, DateTime time)
            {
                MyProperty3 = time;
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
            }
        }

        [Test]
        public void A_class_should_map_properties()
        {
            var time = new DateTime(2001, 1, 1);
            var ret = new MyClass(1, "2").As<MyClass2>(time);
            Assert.Equal(ret.MyProperty, 1);
            Assert.Equal(ret.MyProperty2, "2");
            Assert.Equal(ret.MyProperty3, time);
        }
    }
}
