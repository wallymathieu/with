using System;
using With;
using System.Collections.Generic;
namespace Timing
{
    class TimingsNewClass : TimingsBase
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

            internal MyClass2 AddMinutes(DateTime time)
            {
                return new MyClass2(MyProperty, MyProperty2, time);
            }
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

        public void Timing_equalequal()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3 == time);
            }
        }

        public void Timing_propertyname_only()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3, time);
            }
        }
        public void Timing_dictionary()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass2>(new Dictionary<String, object> { { "MyProperty3", time } });
            }
        }

        public void Timing_ordinal()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass2>(time);
            }
        }

        public void Timing_fluent()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass2>().Eql(p => p.MyProperty3, time)
                    .To(); // use to or cast to get the new instance
            }
        }

        public void Timing_by_hand()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").AddMinutes(time);
            }
        }
    }

}
