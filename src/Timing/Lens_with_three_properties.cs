using System;
using With;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Timing
{
    public class Lens_with_three_properties
    {
        private static readonly Customer _myClass = new Customer(1, "2", new[] { "t"});

        private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> _usingBuiltLens =
            LensBuilder<Customer>.Of(Customer._Id).And(Customer._Name).And(Customer._Preferenses).BuildPreparedCopy();
        private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> _usingHandWrittenLens =
            LensBuilder<Customer>.Of(Customer._IdHand).And(Customer._NameHand).And(Customer._PreferensesHand).BuildPreparedCopy();
        private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> _usingHandWrittenCopyUpdate = new HandWrittenCopyUpdate();
     
        class HandWrittenCopyUpdate : IPreparedCopy<Customer, int, string, IEnumerable<string>>
        {
            public Customer Copy(Customer value1, int id, string name, IEnumerable<string> preferences)
            {
                return new Customer(id,name,preferences);
            }
        }
        [Benchmark]
        public void Using_lens_built_from_library()
        {
            var res = _usingBuiltLens.Copy(_myClass, 2, "2", new[] { "2"});
        }

        [Benchmark]
        public void Hand_written_lenses_composed()
        {
            var res = _usingHandWrittenLens.Copy(_myClass, 2, "2", new[] { "2" });
        }
        [Benchmark]
        public void Hand_written_copy_update_instance()
        {
            var res = _usingHandWrittenCopyUpdate.Copy(_myClass, 2, "2", new[] { "2" });
        }
        /* [Benchmark]
        public void Language_ext_lens()
        {
        } */
    }
}
