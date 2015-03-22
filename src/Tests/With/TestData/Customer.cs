using System;
using System.Collections.Generic;

namespace Tests
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
    }
}
