using System.Collections.Generic;
using With.Collections;
using Xunit;

namespace Tests.Collections
{
    public class DictionaryUsageTests
    {
        [Fact]
        public void When_accessing_through_known_key()
        {
            var used = new List<string>();
            var dictionary = new ReadOnlyDictionaryUsage<string, int>(new Dictionary<string, int> {
                    { "1",1 },
                    { "2",2 }
                },
                (key, value) => { used.Add(key); });
            var _ = dictionary["1"];
            Assert.Equal(new[] { "1" }, used.ToArray());
        }

        [Fact]
        public void When_iterating_through_values()
        {
            var used = new List<string>();
            var dictionary = new ReadOnlyDictionaryUsage<string, int>(new Dictionary<string, int> {
                    { "1",1 },
                    { "2",2 }
                },
                (key, value) => { used.Add(key); });
            var enumerator = dictionary.Values.GetEnumerator();
            enumerator.MoveNext();
            var _ = enumerator.Current;
            Assert.Equal(new[] { "1" }, used.ToArray());
        }
    }
}
