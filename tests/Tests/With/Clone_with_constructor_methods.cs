using System;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using With;
using With.Lenses;
using Xunit;

namespace Tests.With
{
    public class Clone_with_constructor_methods
    {
        public Clone_with_constructor_methods ()
        {
        }
        private static readonly Lazy<DataLens<CSale, string>> CustomerNameCopy = LazyT.Create (() =>
            LensBuilder<CSale>.Constructors(new [] { "MK", "New"}).Of (sp => sp.Customer.Name).Build ());

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_equalequal (CSale myClass, string newValue)
        {
            //var 
            var ret = CustomerNameCopy.Value.Set (myClass, newValue);
            Assert.Equal (newValue, ret.Customer.Name);
            Assert.Equal (myClass.Id, ret.Id);
            Assert.True (ret.FromMK, "from MK");
            Assert.True (ret.Customer.FromNew, "from New");
        }
    }
}
