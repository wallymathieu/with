using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using With;
using Xunit;

namespace Tests.With
{
    public class Compose_lenses_in_csharp
    {
 
        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set(
            Customer myClass, int newValue)
        {
            var ret = Customer._Id.Set(myClass, newValue);
            Assert.Equal(newValue, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set(
            Customer myClass, int newIntValue, string newStrValue)
        {
            var ret = Customer._Id.Combine(Customer._Name).Set(myClass, (newIntValue, newStrValue));
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_three_property_set(
            Customer myClass, int newIntValue, string newStrValue, IEnumerable<string> prefs)
        {
            var ret = Customer._Id.Combine(Customer._Name).Combine(Customer._Preferenses).Set(myClass, 
                ((newIntValue, newStrValue), prefs));
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
            Assert.Equal(prefs, ret.Preferences);
        }
    }
}
