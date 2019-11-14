using System;
using With;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Timing
{
    public class TimingsClone : TimingsBase
    {
        private static readonly MyClass _myClass = new MyClass(1, "2");

        private static readonly IPreparedCopy<MyClass, string> _myClassPreparedCopy =
            Prepare.Copy<MyClass, string>((m, v) => m.MyProperty2 == v);

        public class MyClass
        {
            public MyClass(int myProperty, string myProperty2)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
            }

            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }

            internal MyClass SetMyProperty2(string v)
            {
                return new MyClass(MyProperty, v);
            }
        }

        [Benchmark]
        public void Timing_equalequal()
        {
            var time = new DateTime(2001, 1, 1).AddMinutes(2);
            var res = _myClassPreparedCopy.Copy(_myClass, time.ToString());
        }

        [Benchmark]
        public void Timing_by_hand()
        {
            var time = new DateTime(2001, 1, 1).AddMinutes(2);
            var res = _myClass.SetMyProperty2(time.ToString());
        }
    }
}
