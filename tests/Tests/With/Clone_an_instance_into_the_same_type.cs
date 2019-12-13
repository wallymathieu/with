using System;
using System.Collections.Generic;
using Tests.With.TestData;
using With;
using With.Lenses;
using Xunit;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;

namespace Tests.With
{
    public class Clone_an_instance_into_the_same_type
    {
        private static readonly Lazy<DataLens<Customer, int>> IdCopy = LazyT.Create(() =>
            LensBuilder<Customer>.Of(m => m.Id ).Build());
        private static readonly Lazy<DataLens<Customer, (int, string)>> IdAndNameCopy = LazyT.Create(() =>
             LensBuilder<Customer>.Of<int, string>((m, v1, v2) => m.Id == v1 && m.Name == v2).Build());
        private static readonly Lazy<DataLens<Customer, (int, string, IEnumerable<string>)>> IdAndNameAndPreferencesCopy = LazyT.Create(() =>
            LensBuilder<Customer>.Of<int, string>((m, v1, v2) => m.Id == v1 && m.Name == v2)
                          .And<IEnumerable<string>>((m, v) => m.Preferences == v)
                          .Build());

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal(
            Customer myClass, int newValue)
        {
            var ret = IdCopy.Value.Set(myClass, newValue);
            Assert.Equal(newValue, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal(
            Customer myClass, int newIntValue, string newStrValue)
        {
            var ret = IdAndNameCopy.Value.Set(myClass, (newIntValue, newStrValue));
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_three_property_set_using_equal_equal(
            Customer myClass, int newIntValue, string newStrValue, IEnumerable<string> prefs)
        {
            var ret = IdAndNameAndPreferencesCopy.Value.Set(myClass, (newIntValue, newStrValue, prefs));
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
            Assert.Equal(prefs, ret.Preferences);
        }
        private static readonly Lazy<DataLens<CustomerWithEmptyCtor, (int, string)>> EmptyCtorIdAndNameCopy = LazyT.Create(() =>
             LensBuilder<CustomerWithEmptyCtor>.Of<int,string>((m, v1, v2) => m.Id == v1 && m.Name == v2).Build());

        [Theory, AutoData]
        public void A_class_with_empty_ctor(
            CustomerWithEmptyCtor instance, int newInt, string newString)
        {
            var ret = EmptyCtorIdAndNameCopy.Value.Set(instance, (newInt, newString));
            Assert.Equal(newInt, ret.Id);
            Assert.Equal(newString, ret.Name);
        }
        public class CustomerWithEmptyCtor : Customer
        {
            public CustomerWithEmptyCtor()
                : this(-1, null, new string[0])
            {
            }
            public CustomerWithEmptyCtor(int id, string name, IEnumerable<string> preferences)
                : base(id, name, preferences)
            {
            }
        }
    }
}
