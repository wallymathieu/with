using System;
using System.Collections.Immutable;

namespace Tests.With.TestData
{
    public class CustomerWithImmutablePreferences
    {
        private readonly int id;
        private readonly string name;
        private readonly IImmutableList<string> preferences;
        public CustomerWithImmutablePreferences(int id, string name, IImmutableList<string> preferences)
        {
            this.id = id;
            this.name = name;
            this.preferences = preferences;
        }
        public int Id { get { return id; } private set { throw new Exception(); } }
        public string Name { get { return name; } private set { throw new Exception(); } }
        public IImmutableList<string> Preferences { get { return preferences; } private set { throw new Exception(); } }
    }
}
