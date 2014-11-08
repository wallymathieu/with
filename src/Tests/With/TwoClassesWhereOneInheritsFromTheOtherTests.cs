using System;
using System.Collections.Generic;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
using With;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Tests
{
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

        [Theory, AutoData]
        public void A_class_should_map_its_parents_properties(MyClass myClass, DateTime time)
        {
            var ret = myClass.As<MyClass2>(time);
            Assert.Equal(ret.MyProperty, myClass.MyProperty);
            Assert.Equal(ret.MyProperty2, myClass.MyProperty2);
            Assert.Equal(ret.MyProperty3, time);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_use_lambda(MyClass myClass, DateTime time)
        {
            var ret = myClass.As<MyClass2>(m => m.MyProperty3 == time);
            Assert.Equal(ret.MyProperty, myClass.MyProperty);
            Assert.Equal(ret.MyProperty2, myClass.MyProperty2);
            Assert.Equal(ret.MyProperty3, time);
        }

        [Theory, AutoData]
        public void A_class_using_cast(MyClass myClass, DateTime time)
        {
            Object _time = (Object)time;
            var ret = myClass.As<MyClass2>(m => m.MyProperty3 == (DateTime)_time);
            Assert.Equal(ret.MyProperty, myClass.MyProperty);
            Assert.Equal(ret.MyProperty2, myClass.MyProperty2);
            Assert.Equal(ret.MyProperty3, time);
        }

    }
}
