using System;
using With;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using With.Lenses;

namespace Timing
{
    public class Lens_with_three_properties
    {
        private static readonly Customer _myClass = new Customer(1, "2", new[] { "t"});

        private static readonly DataLens<Customer, (int, string, IEnumerable<string>)> _usingBuiltLens =
            LensBuilder<Customer>.Of(Customer._Id).And(Customer._Name).And(Customer._Preferenses).Build();
        private static readonly DataLens<Customer, (int, string, IEnumerable<string>)> _usingHandWrittenLens =
            LensBuilder<Customer>.Of(Customer._IdHand).And(Customer._NameHand).And(Customer._PreferensesHand).Build();
        private static readonly DataLens<Customer, (int, string, IEnumerable<string>)> _usingHandWrittenCopyUpdate = HandWrittenCopyUpdate();
     
        static DataLens<Customer, (int, string, IEnumerable<string>)> HandWrittenCopyUpdate()
        {
            return DataLens< Customer, (int, string, IEnumerable<string>)>.Create(c => (c.Id,c.Name,c.Preferences), (_,item) => new Customer(item.Item1, item.Item2, item.Item3));
        }
        [Benchmark]
        public void Using_lens_built_from_library()
        {
            var res = _usingBuiltLens.Set(_myClass, (2, "2", new[] { "2"}));
        }

        [Benchmark]
        public void Hand_written_lenses_composed()
        {
            var res = _usingHandWrittenLens.Set(_myClass, (2, "2", new[] { "2" }));
        }
        [Benchmark]
        public void Hand_written_copy_update_instance()
        {
            var res = _usingHandWrittenCopyUpdate.Set(_myClass, (2, "2", new[] { "2" }));
        }
        /* [Benchmark]
        public void Language_ext_lens()
        {
        } */
    }
}
