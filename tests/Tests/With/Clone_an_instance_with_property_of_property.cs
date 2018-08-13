using System;
using With;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;
using Assert = Xunit.Assert;
using Xunit;

namespace Tests
{
    public class Clone_an_instance_with_property_of_property
    {
        private static readonly Lazy<IPreparedCopy<Sale, string>> CustomerNameCopy = new Lazy<IPreparedCopy<Sale, string>>(()=>
            Prepare.Copy<Sale,string>((sp,v) => sp.Customer.Name == v));

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set(
            Sale myClass, string newValue)
        {
            var ret = myClass.With(sp => sp.Customer.Name, newValue);
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }

        [Theory(Skip = "not implemented"), AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_equalequal(
Sale myClass, string newValue)
        {
            var ret = CustomerNameCopy.Value.Copy(myClass, newValue);
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_with_a_property_set_using_Eql(
Sale myClass, string newValue)
        {
            var ret = myClass.With().Eql(sp => sp.Customer.Name, newValue)
                .Copy();
            Assert.Equal(newValue, ret.Customer.Name);
            Assert.Equal(myClass.Id, ret.Id);
        }
    }
}
