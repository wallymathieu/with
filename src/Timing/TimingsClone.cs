using System;
using With;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Timing
{
    public class TimingsClone : TimingsBase
    {

        public class MyClass
        {
            public MyClass (int myProperty, string myProperty2)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
            }
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }

            internal MyClass SetMyProperty2 (string v)
            {
                return new MyClass (MyProperty, v);
            }
        }

        [Benchmark]
        public void Timing_equalequal ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            new MyClass (1, "2").With (m => m.MyProperty2 == time.ToString ());
        }
        [Benchmark]
        public void Timing_propertyname_only ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            new MyClass (1, "2").With (m => m.MyProperty2, time.ToString ());
        }
        [Benchmark]
        public void Timing_dictionary ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            new MyClass (1, "2").With (new Dictionary<String, object> { { "MyProperty2", time.ToString () } });
        }
        [Benchmark]
        public void Timing_fluent ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            new MyClass (1, "2").With ().Eql (p => p.MyProperty2, time.ToString ())
                .To (); // use to or cast to get the new instance
        }

        [Benchmark]
        public void Timing_by_hand ()
        {
            var time = new DateTime (2001, 1, 1).AddMinutes (2);
            new MyClass (1, "2").SetMyProperty2 (time.ToString ());
        }
    }

}
