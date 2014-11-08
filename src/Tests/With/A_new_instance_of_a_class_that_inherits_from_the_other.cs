using System;
using System.Collections.Generic;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
using With;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace Tests
{
    public class A_new_instance_of_a_class_that_inherits_from_the_other
    {
        public class MyClass2 : MyClass
        {
            private readonly DateTime myProperty3;
            public MyClass2(int myProperty, string myProperty2, DateTime myProperty3)
                : base(myProperty, myProperty2)
            {
                this.myProperty3 = myProperty3;
            }
            public DateTime MyProperty3 { get { return myProperty3; } private set { throw new Exception(); } }

        }

        [Theory, AutoData]
        public void A_class_should_map_its_parents_properties(MyClass myClass, DateTime time)
        {
            var ret = myClass.As<MyClass2>(time);
            Assert.Equal(time, ret.MyProperty3);

            Assert.Equal(myClass.MyProperty, ret.MyProperty);
            Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_use_lambda(MyClass myClass, DateTime time)
        {
            var ret = myClass.As<MyClass2>(m => m.MyProperty3 == time);
            Assert.Equal(ret.MyProperty3, time);

            Assert.Equal(myClass.MyProperty, ret.MyProperty);
            Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
        }

        [Theory, AutoData]
        public void A_class_using_cast(MyClass myClass, DateTime time)
        {
            Object _time = (Object)time;
            var ret = myClass.As<MyClass2>(m => m.MyProperty3 == (DateTime)_time);
            Assert.Equal(ret.MyProperty3, time);

            Assert.Equal(myClass.MyProperty, ret.MyProperty);
            Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
        }
    }
}
