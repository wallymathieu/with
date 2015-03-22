using Xunit;
using System.Linq;
using System.Collections.Generic;
using Xunit.Extensions;
using With;
using With.ReadonlyEnumerable;
using Tests.With.TestData;

namespace Tests.With
{
    public class Manipulation_of_readonly_dictionary
    {
        [Theory, ReadonlyDictionaryData]
        public void Should_be_able_to_add_and_remove_from_dictionary(ClassWithFields models, Customer myclass, int key)
        {
            var newModels = models.With(o => o.MyDictionary.Add(key, myclass));
            Assert.Equal(myclass, newModels.MyDictionary[key]);
            var newNewModels = models.With(o => o.MyDictionary.Remove(key));
            Assert.False(newNewModels.MyDictionary.ContainsKey(key));
        }

        [Theory, ReadonlyDictionaryData]
        public void Should_be_able_to_add_and_replace_for_dictionary(ClassWithFields models, Customer myclass, int key, Customer replacement)
        {
            var newModels = models.With(o => o.MyDictionary.Add(key, myclass));
            Assert.Equal(myclass, newModels.MyDictionary[key]);
            var newNewModels = models.With(o => o.MyDictionary.Replace(key, replacement));
            Assert.Equal(replacement, newNewModels.MyDictionary[key]);
        }

        public class ClassWithFields
        {
            public ClassWithFields(IReadOnlyDictionary<int, Customer> myDictionary)
            {
                MyDictionary = myDictionary;
            }

            public readonly IReadOnlyDictionary<int, Customer> MyDictionary;
        }
    }
}
