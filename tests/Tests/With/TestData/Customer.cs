using System;
using System.Collections.Generic;
using With;
using With.Lenses;

namespace Tests.With.TestData
{
    public class Customer
    {
        private readonly int id;
        private readonly string name;
        private readonly IEnumerable<string> preferences;
        public Customer(int id, string name, IEnumerable<string> preferences)
        {
            this.id = id;
            this.name = name;
            this.preferences = preferences;
        }
        public int Id { get { return id; } private set { throw new Exception(); } }
        public string Name { get { return name; } private set { throw new Exception(); } }
        public IEnumerable<string> Preferences { get { return preferences; } private set { throw new Exception(); } }

        public static DataLens<Customer,int> _Id = LensBuilder<Customer>.Of(c=>c.Id).Build();
        public static DataLens<Customer,string> _Name = LensBuilder<Customer>.Of(c=>c.Name).Build();
        public static DataLens<Customer,IEnumerable<string>> _Preferenses = LensBuilder<Customer>.Of(c=>c.Preferences).Build();
    }
}
