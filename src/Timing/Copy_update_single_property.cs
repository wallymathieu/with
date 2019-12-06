using System;
using With;
using BenchmarkDotNet.Attributes;

namespace Timing
{
    public class Copy_update_single_property
    {
        private static readonly Customer _myClass = new Customer(1, "2", new[] { "t" });

        private static readonly IPreparedCopy<Customer, string> _myClassPreparedCopy =
            Prepare.Copy<Customer, string>((m, v) => m.Name == v);


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
            var res = _myClass.SetName(time.ToString());
        }
        [Benchmark]
        public void Language_ext_generated()
        {
            var time = new DateTime(2001, 1, 1).AddMinutes(2);
            var res = _myClass.With(Name:time.ToString());
        }
    }
}
