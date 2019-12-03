using System;
using With;
using BenchmarkDotNet.Attributes;

namespace Timing
{
    public class Copy_update_single_property
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
        public void Using_static_prepered_copy_expression()
        {
            var time = new DateTime(2001, 1, 1).AddMinutes(2);
            var res = _myClassPreparedCopy.Copy(_myClass, time.ToString());
        }

        [Benchmark]
        public void Hand_written_method_returning_new_instance()
        {
            var time = new DateTime(2001, 1, 1).AddMinutes(2);
            var res = _myClass.SetMyProperty2(time.ToString());
        }
    }
}
