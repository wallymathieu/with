using System;
using Tests.With.TestData;
using With;
using Xunit;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;
using Assert = Xunit.Assert;

namespace Tests.With
{
    public class Clone_an_instance_with_property_of_property
    {
        private static readonly Lazy<IPreparedCopy<Sale, string>> CustomerNameCopy = new Lazy<IPreparedCopy<Sale, string>>(()=>
            Prepare.Copy<Sale,string>((sp,v) => sp.Customer.Name == v));

        [Theory(Skip = "not implemented"), AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_equalequal(Sale myClass, string newValue)
        {
            var ret = CustomerNameCopy.Value.Copy(myClass, newValue);
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }
        private static readonly Lazy<IPreparedCopy<Sale, string>> CustomerNameComposeCopy = new Lazy<IPreparedCopy<Sale, string>>(() =>
            LensBuilder<Sale>.Of(sp=>sp.Customer).Then(c => c.Name).BuildPreparedCopy());

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_compose(Sale myClass, string newValue)
        {
            var ret = CustomerNameComposeCopy.Value.Copy(myClass, newValue);
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }
    }
}
