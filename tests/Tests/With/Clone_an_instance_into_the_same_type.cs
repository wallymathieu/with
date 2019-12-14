using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using With;
using With.Lenses;
using Xunit;

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
        public void A_class_with_empty_ctor_and_a_longer_ctor_should_use_the_one_with_the_most_parameters(
            CustomerWithEmptyCtor instance, int newInt, string newString)
        {
            var ret = EmptyCtorIdAndNameCopy.Value.Set(instance, (newInt, newString));
            Assert.Equal(newInt, ret.Id);
            Assert.Equal(newString, ret.Name);
        }
        [Fact]
        public void A_class_with_a_missing_constructor_parameter_should_give_an_exception_on_build()
        {
            Assert.Throws<MissingConstructorParameterException> (()=>
                LensBuilder<CustomerWithMissingConstructorParameter>.Of( m=> m.Id).And(m=>m.Name).Build());
        }
    }
}
