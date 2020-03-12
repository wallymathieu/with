using System;
using System.Collections.Generic;

namespace Tests.With.TestData
{
    public class CCustomer
    {
        public bool FromNew { get; private set; }
        private readonly int id;
        private readonly string name;
        private readonly IEnumerable<string> preferences;
        private CCustomer (int id, string name, IEnumerable<string> preferences)
        {
            this.id = id;
            this.name = name;
            this.preferences = preferences;
            FromNew = false;
        }
        public int Id { get { return id; } private set { throw new Exception (); } }
        public string Name { get { return name; } private set { throw new Exception (); } }
        public IEnumerable<string> Preferences { get { return preferences; } private set { throw new Exception (); } }
        public static CCustomer New (int id, string name, IEnumerable<string> preferences)
        {
            return new CCustomer (id, name, preferences) { FromNew =true };
        }
    }
}
