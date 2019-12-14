using System;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using With;
using Xunit;
using Assert = Xunit.Assert;
using With.Lenses;

namespace Tests.With
{
    public class Clone_an_instance_with_property_of_property
    {
        private static readonly Lazy<DataLens<Sale, string>> CustomerNameCopy = LazyT.Create(()=>
            LensBuilder<Sale>.Of(sp => sp.Customer.Name).Build());

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_equalequal(Sale myClass, string newValue)
        {
            var ret = CustomerNameCopy.Value.Set(myClass, newValue);
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }
        private static readonly Lazy<DataLens<Sale, string>> CustomerNameComposeCopy = LazyT.Create(() =>
            LensBuilder<Sale>.Of(sp=>sp.Customer).Then(c => c.Name).Build());

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_compose(Sale myClass, string newValue)
        {
            var ret = CustomerNameComposeCopy.Value.Set(myClass, newValue);
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }
    }
}
