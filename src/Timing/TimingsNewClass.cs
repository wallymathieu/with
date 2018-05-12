using System;
using With.Coercions;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Timing
{
    public class TimingsNewClass : TimingsBase
    {
        private static readonly MyClass _myClass = new MyClass (1, "2");

        public class MyClass
        {
            public MyClass (int myProperty, string myProperty2)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
            }
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }

            internal MyClass2 AddMinutes (DateTime time)
            {
                return new MyClass2 (MyProperty, MyProperty2, time);
            }
        }

        public class MyClass2 : MyClass
        {
            public DateTime MyProperty3 { get; set; }
            public MyClass2 (int myProperty, string myProperty2, DateTime myProperty3)
                : base (myProperty, myProperty2)
            {
                MyProperty3 = myProperty3;
            }
        }

/*        [Benchmark]
        public void Timing_equalequal ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            var ret = new MyClass (1, "2").As<MyClass, MyClass2,DateTime> (m => m.MyProperty3 == time);
        }*/
        [Benchmark]
        public void Timing_propertyname_only ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            var ret = _myClass.As<MyClass, MyClass2,DateTime> (m => m.MyProperty3, time);
        }
        [Benchmark]
        public void Timing_dictionary ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            var ret = _myClass.As<MyClass, MyClass2> (new Dictionary<String, object> { { "MyProperty3", time } });
        }

        [Benchmark]
        public void Timing_ordinal ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            var orig = _myClass;
            var ret = new MyClass2 (orig.MyProperty, orig.MyProperty2, time);
        }

        [Benchmark]
        public void Timing_fluent ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            var ret = _myClass.As<MyClass, MyClass2> ().Eql (p => p.MyProperty3, time)
                .Copy (); // use to or cast to get the new instance
        }

        [Benchmark]
        public void Timing_by_hand ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            var ret = _myClass.AddMinutes (time);
        }
    }

}
