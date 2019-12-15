using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using With;
using With.Lenses;
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
            Assert.Equal(newValue, Customer._Id.Get(ret));
        }
        [Theory, AutoData]
        public void Should_be_able_to_update_price(Sale sale)
        {
            var lens=Product._Price.Compose(Sale._Product);
            var expectedPrice = sale.Product.Price+1;
            var ret = lens.Update(price=>price+1, sale);
            Assert.Equal(expectedPrice, ret.Product.Price);
            Assert.Equal(expectedPrice, lens.Get(ret));
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set(
            Customer myClass, int newIntValue, string newStrValue)
        {
            var lens = Customer._Id.Combine(Customer._Name);
            var arg=(newIntValue, newStrValue);
            var ret = lens.Set(myClass, arg);
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
            Assert.Equal(arg, lens.Get(ret));
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_three_property_set(
            Customer myClass, int newIntValue, string newStrValue, IEnumerable<string> prefs)
        {
            var lens = Customer._Id.Combine(Customer._Name).Combine(Customer._Preferenses);
            var arg=((newIntValue, newStrValue), prefs);
            var ret = lens.Set(myClass,arg);
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
            Assert.Equal(prefs, ret.Preferences);
            Assert.Equal(arg, lens.Get(ret));
        }
    }
}
