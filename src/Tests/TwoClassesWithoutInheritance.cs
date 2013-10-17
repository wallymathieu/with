using System;
using NUnit.Framework;
using With;
namespace Tests
{
    [TestFixture]
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
            var ret = new MyClass(1, "2").With<MyClass2>(time);
            Assert.That(ret.MyProperty, Is.EqualTo(1));
            Assert.That(ret.MyProperty2, Is.EqualTo("2"));
            Assert.That(ret.MyProperty3, Is.EqualTo(time));
        }
    }
}
