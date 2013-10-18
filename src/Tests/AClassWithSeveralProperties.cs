using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using With;
namespace Tests
{
    [TestFixture]
    public class AClassWithSeveralProperties
    {
        public class MyClass
        {
            public MyClass(int myProperty, string myProperty2, string myProperty3, string myProperty4)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
                MyProperty3 = myProperty3;
                MyProperty4 = myProperty4;
            }
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }
            public string MyProperty3 { get; private set; }
            public string MyProperty4 { get; private set; }
        }

        [Test]
        public void A_class_should_be_able_to_create_a_clone_with_several_properties_set_using_equal_equal()
        {
            var ret = new MyClass(1, "1","2","3").With(m => 
                m.MyProperty == 7 
                && m.MyProperty2 == "8"
                && m.MyProperty3 == "9"
                && m.MyProperty4 == "10");
            Assert.True(ret.MyProperty == 7
                && ret.MyProperty2 == "8"
                && ret.MyProperty3 == "9"
                && ret.MyProperty4 == "10");
        }
    }
}
