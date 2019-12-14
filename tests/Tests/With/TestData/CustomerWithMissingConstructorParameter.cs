using System;
using System.Collections.Generic;

namespace Tests.With.TestData
{
    public class CustomerWithMissingConstructorParameter
    {
        private readonly int id;
        private readonly string name;
        private readonly IEnumerable<string> preferences;
        public CustomerWithMissingConstructorParameter(int id,  IEnumerable<string> preferences)
        {
            this.id = id;
            this.name = "named";
            this.preferences = preferences;
        }
        public int Id { get { return id; } private set { throw new Exception(); } }
        public string Name { get { return name; } private set { throw new Exception(); } }
        public IEnumerable<string> Preferences { get { return preferences; } private set { throw new Exception(); } }
    }
}
