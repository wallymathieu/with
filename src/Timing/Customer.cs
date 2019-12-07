using With;
using System.Collections.Generic;
using With.Lenses;
using LanguageExt;

namespace Timing
{
    [WithLens]
    public partial class Customer
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
        public static DataLens<Customer, int> _IdHand = DataLens<Customer, int>.Create(c => c.Id, (c, v) => c.SetId(v));
        public static DataLens<Customer, string> _NameHand = DataLens<Customer, string>.Create(c => c.Name, (c, v) => c.SetName(v));
        public static DataLens<Customer, IEnumerable<string>> _PreferensesHand = DataLens<Customer, IEnumerable<string>>.Create(c => c.Preferences, (c, v) => c.SetPreferences(v));
    }
}
