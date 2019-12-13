using System;
using With;
using BenchmarkDotNet.Attributes;
using With.Lenses;

namespace Timing
{
    public class Copy_update_single_property
    {
        private static readonly Customer _myClass = new Customer(1, "2", new[] { "t" });

        private static readonly DataLens<Customer, string> _myClassPreparedCopy =
            LensBuilder<Customer>.Of(m => m.Name).Build();


        [Benchmark]
        public void Using_static_prepered_copy_expression()
        {
            var time = new DateTime(2001, 1, 1).AddMinutes(2);
            var res = _myClassPreparedCopy.Set(_myClass, time.ToString());
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
