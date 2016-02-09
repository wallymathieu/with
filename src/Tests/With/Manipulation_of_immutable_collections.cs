using Xunit;
using Xunit.Extensions;
using System.Linq;
using With;
using With.ReadonlyEnumerable;
using System.Collections.Generic;
using System;
using Customer = Tests.With.TestData.CustomerWithImmutablePreferences;
using FlyFishingBuddyCustomer = Tests.With.TestData.FlyFishingBuddy<Tests.With.TestData.CustomerWithImmutablePreferences>;
using System.Collections.Immutable;
using AutoDataAttribute = Tests.With.TestData.ImmutableAutoDataAttribute;

namespace Tests.With
{
    public class Manipulation_of_immutable_collections
    {
        [Theory, AutoData]
        public void Should_be_able_to_add_to_enumerable(
            Customer myClass, string newValue)
        {
            var ret = myClass.With(m => m.Preferences.Add(newValue));
            Assert.Equal(newValue, ret.Preferences.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_union_add_to_enumerable(
            Customer myClass, string newValue)
        {
            var ret = myClass.With(m => m.Preferences.Union(new[] { newValue }));
            Assert.Equal(newValue, ret.Preferences.Last());

            var array = new[] { newValue };
            ret = myClass.With(m => m.Preferences.Union(array));
            Assert.Equal(newValue, ret.Preferences.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_concat_add_to_enumerable(
            Customer myClass, string newValue)
        {
            var ret = myClass.With(m => m.Preferences.Concat(new[] { newValue }));
            Assert.Equal(newValue, ret.Preferences.Last());

            var array = new[] { newValue };
            ret = myClass.With(m => m.Preferences.Concat(array));
            Assert.Equal(newValue, ret.Preferences.Last());
        }

        [Theory, AutoData]
        public void Able_to_set_array_to_empty_array(
            Customer myClass, string newValue)
        {
            var ret = myClass.With(m => m.Preferences == ImmutableList<string>.Empty);
            Assert.Equal(ImmutableList<string>.Empty, ret.Preferences);
        }

        [Theory, AutoData]
        public void Should_be_able_to_add_const_to_enumerable(
            Customer myClass)
        {
            const string newValue = "const";
            var ret = myClass.With(m => m.Preferences.Add(newValue));
            Assert.Equal(newValue, ret.Preferences.Last());

            ret = myClass.With(m => m.Preferences.Concat(new[] { newValue }));
            Assert.Equal(newValue, ret.Preferences.Last());

            var array = new[] { newValue };
            ret = myClass.With(m => m.Preferences.Concat(array));
            Assert.Equal(newValue, ret.Preferences.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_add_range_with_new_array_to_enumerable(
            Customer myClass, string newValue)
        {
            var ret = myClass.With(m => m.Preferences.AddRange(new[] { newValue }));
            Assert.Equal(newValue, ret.Preferences.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_add_range_to_enumerable(
            Customer myClass, IEnumerable<string> newValue)
        {
            var ret = myClass.With(m => m.Preferences.AddRange(newValue));
            Assert.Equal(newValue.Last(), ret.Preferences.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_remove_from_enumerable(
            Customer myClass)
        {
            var first = myClass.Preferences.First();
            var ret = myClass.With(m => m.Preferences.Remove(first));
            Assert.NotEqual(first, ret.Preferences.First());
        }

        [Theory, AutoData]
        public void Should_be_able_to_where_remove_from_enumerable(
            Customer myClass)
        {
            var first = myClass.Preferences.First();
            var ret = myClass.With(m => m.Preferences.Where(p => p != first));
            Assert.NotEqual(first, ret.Preferences.First());
        }

        public class AllOurCustomers
        {
            public AllOurCustomers()
                : this(ImmutableList<Customer>.Empty, ImmutableList<FlyFishingBuddyCustomer>.Empty)
            {
            }
            public AllOurCustomers(IImmutableList<Customer> myClasses, IImmutableList<FlyFishingBuddyCustomer> myClassWIthObjects)
            {
                MyClasses = myClasses;
                MyClassWIthObjects = myClassWIthObjects;
            }

            public readonly IImmutableList<Customer> MyClasses;
            public readonly IImmutableList<FlyFishingBuddyCustomer> MyClassWIthObjects;
        }

        [Theory, AutoData]
        public void Should_be_able_to_set_enumerable_on_model_with_empty_constructor(
            AllOurCustomers models)
        {
            var myClass = new Customer(-1, string.Empty, ImmutableList<string>.Empty);
            var ret = models.With(m => m.MyClasses.Add(myClass));
            Assert.Equal(myClass, ret.MyClasses.First());
        }

        [Theory, AutoData]
        public void Should_be_able_to_set_enumerable_with_new(AllOurCustomers models)
        {
            var ret = models.With(m => m.MyClasses.Add(new Customer(-1, string.Empty, ImmutableList<string>.Empty)));
            Assert.Equal(-1, ret.MyClasses.First().Id);
        }
    }
}
