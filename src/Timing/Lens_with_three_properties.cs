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

        private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> _usingBuiltLens =
            LensBuilder<Customer>.Of(Customer._Id).And(Customer._Name).And(Customer._Preferenses).BuildPreparedCopy();
        private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> _usingHandWrittenLens =
            LensBuilder<Customer>.Of(Customer._IdHand).And(Customer._NameHand).And(Customer._PreferensesHand).BuildPreparedCopy();
        private static readonly IPreparedCopy<Customer, int, string, IEnumerable<string>> _usingHandWrittenCopyUpdate = new HandWrittenCopyUpdate();
        public class Customer
        {
            public Customer(int id, string name, IEnumerable<string> preferences)
            {
                this.Id = id;
                this.Name = name;
                this.Preferences = preferences;
            }
            public int Id { get; }
            public string Name { get; }
            public IEnumerable<string> Preferences { get; }

            internal Customer SetId(int v)
            {
                return new Customer(v, Name, Preferences);
            }
            internal Customer SetName(string v)
            {
                return new Customer(Id, v, Preferences);
            }
            internal Customer SetPreferences(IEnumerable<string> v)
            {
                return new Customer(Id, Name, v);
            }
            public static DataLens<Customer, int> _Id = LensBuilder<Customer>.Of(c => c.Id).Build();
            public static DataLens<Customer, string> _Name = LensBuilder<Customer>.Of(c => c.Name).Build();
            public static DataLens<Customer, IEnumerable<string>> _Preferenses = LensBuilder<Customer>.Of(c => c.Preferences).Build();
            public static DataLens<Customer, int> _IdHand = DataLens<Customer, int>.Create(c=>c.Id,(c,v)=>c.SetId(v));
            public static DataLens<Customer, string> _NameHand = DataLens<Customer, string>.Create(c => c.Name, (c, v) => c.SetName(v));
            public static DataLens<Customer, IEnumerable<string>> _PreferensesHand = DataLens<Customer,IEnumerable<string>>.Create(c => c.Preferences, (c, v) => c.SetPreferences(v));
        }
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
    }
}
