using System;
using System.Collections.Generic;
using NUnit.Framework;
using With;
namespace Tests
{
    [TestFixture]
    public class TwoClassesWhereOneInheritsFromTheOtherTests
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

        public class MyClass2 : MyClass
        {
            public DateTime MyProperty3 { get; set; }
            public MyClass2(int myProperty, string myProperty2, DateTime myProperty3)
                : base(myProperty, myProperty2)
            {
                MyProperty3 = myProperty3;
            }
        }

        [Test]
        public void A_class_should_map_its_parents_properties()
        {
            var time = new DateTime(2001, 1, 1);
            var ret = new MyClass(1, "2").As<MyClass2>(time);
            Assert.That(ret.MyProperty, Is.EqualTo(1));
            Assert.That(ret.MyProperty2, Is.EqualTo("2"));
            Assert.That(ret.MyProperty3, Is.EqualTo(time));
        }

        [Test]
        public void A_class_should_be_able_to_use_lambda()
        {
            var time = new DateTime(2001, 1, 1);
            var ret = new MyClass(1, "2").As<MyClass2>(m=>m.MyProperty3==time);
            Assert.That(ret.MyProperty, Is.EqualTo(1));
            Assert.That(ret.MyProperty2, Is.EqualTo("2"));
            Assert.That(ret.MyProperty3, Is.EqualTo(time));
        }
        
        [Test]
        public void A_class_using_cast()
        {
            Object time = new DateTime(2001, 1, 1);
            var ret = new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3 == (DateTime)time);
            Assert.That(ret.MyProperty, Is.EqualTo(1));
            Assert.That(ret.MyProperty2, Is.EqualTo("2"));
            Assert.That(ret.MyProperty3, Is.EqualTo(time));
        }

        [Test]
        public void A_time_equalequal()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                var ret = new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3 == time);
            }
        }
        [Test]
        public void A_time_propertyname_only()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                var ret = new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3,time);
            }
        }
        [Test]
        public void A_time_dictionary()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                var ret = new MyClass(1, "2").As<MyClass2>(new Dictionary<String, object> { {"MyProperty3",time} });
            }
        }
        
        [Test]
        public void A_time_ordinal()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                var ret = new MyClass(1, "2").As<MyClass2>(time);
            }
        }
    }
}
