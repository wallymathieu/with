using System;
using System.Collections.Generic;
using Tests.With.TestData;
using With;
using Xunit;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;

namespace Tests.With
{
    public class Compose_lenses_in_csharp
    {
 
        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set(
            Customer myClass, int newValue)
        {
            var ret = Customer._Id.Write(myClass, newValue);
            Assert.Equal(newValue, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set(
            Customer myClass, int newIntValue, string newStrValue)
        {
            var ret = Customer._Id.Combine(Customer._Name).Write(myClass, Tuple.Create(newIntValue, newStrValue));
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_three_property_set(
            Customer myClass, int newIntValue, string newStrValue, IEnumerable<string> prefs)
        {
            var ret = Customer._Id.Combine(Customer._Name).Combine(Customer._Preferenses).Write(myClass, 
                Tuple.Create(Tuple.Create(newIntValue, newStrValue), prefs));
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
            Assert.Equal(prefs, ret.Preferences);
        }
    }
}