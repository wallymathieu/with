using Xunit;
using System.Linq;
using Ploeh.AutoFixture.Xunit2;
using With;
using With.ReadonlyEnumerable;
using System.Collections.Generic;
using System;
using Tests.With.TestData;

namespace Tests.With
{
    public class Manipulation_of_enumerable
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
            var ret = myClass.With(m => m.Preferences == new string[0]);
            Assert.Equal(new string[0], ret.Preferences.ToArray());
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
                : this(new Customer[0], new FlyFishingBuddy<Customer>[0])
            {
            }
            public AllOurCustomers(IEnumerable<Customer> myClasses, IEnumerable<FlyFishingBuddy<Customer>> myClassWIthObjects)
            {
                MyClasses = myClasses;
                MyClassWIthObjects = myClassWIthObjects;
            }

            public readonly IEnumerable<Customer> MyClasses;
            public readonly IEnumerable<FlyFishingBuddy<Customer>> MyClassWIthObjects;
        }

        [Theory, AutoData]
        public void Should_be_able_to_set_enumerable_on_model_with_empty_constructor(
            AllOurCustomers models)
        {
            var myClass = new Customer(-1, string.Empty, new string[0]);
            var ret = models.With(m => m.MyClasses.Add(myClass));
            Assert.Equal(myClass, ret.MyClasses.First());
        }

        [Theory, AutoData]
        public void Should_be_able_to_set_enumerable_with_new(AllOurCustomers models)
        {
            var ret = models.With(m => m.MyClasses.Add(new Customer(-1, string.Empty, new string[0])));
            Assert.Equal(-1, ret.MyClasses.First().Id);
        }

        [Theory, AutoData]
        public void Should_be_able_to_use_call_in_expression(AllOurCustomers models, IEnumerable<Customer> myclasses)
        {
            var newModels = models.With(o => o.MyClasses.Add(myclasses.First()));
            Assert.Equal(myclasses.First(), newModels.MyClasses.Last());
        }

        [Theory, AutoData]
        public void Should_be_able_to_use_invoke_in_expression(AllOurCustomers models, IEnumerable<Customer> myclasses)
        {
            Func<Customer> getMyClass = () => myclasses.First();
            var newModels = models.With(o => o.MyClasses.Add(getMyClass()));
            Assert.Equal(getMyClass(), newModels.MyClasses.Last());
        }

        //Not implemented [Theory, AutoData]
        public void Should_be_able_to_use_coalesce_in_expression(IEnumerable<Customer> myclasses)
        {
            var models = new AllOurCustomers(null, null);
            var newModels = models.With(o => o.MyClasses ?? myclasses);
            Assert.Equal(myclasses, newModels.MyClasses);
        }
    }
}
