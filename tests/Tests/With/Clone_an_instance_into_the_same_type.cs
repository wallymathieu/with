using System;
using With;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class Clone_an_instance_into_the_same_type
    {
        private static readonly Lazy<IPreparedCopy<Customer, int>> IdCopy = new Lazy<IPreparedCopy<Customer, int>>(()=> 
            Prepare.Copy<Customer,int>((m,v) => m.Id == v));
        private static readonly Lazy<IPreparedCopy<Customer, int, string>> IdAndNameCopy =new Lazy<IPreparedCopy<Customer, int, string>>(()=> 
            Prepare.Copy<Customer,int,string>((m,v1,v2) => m.Id == v1 && m.Name==v2));

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set(
            Customer myClass, int newValue)
        {
            var ret = myClass.With(m => m.Id, newValue);
            Assert.Equal(newValue, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }
        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal(
            Customer myClass, int newValue)
        {
            var ret = IdCopy.Value.Copy(myClass, newValue);
            Assert.Equal(newValue, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal(
            Customer myClass, int newIntValue, string newStrValue)
        {
            var ret = IdAndNameCopy.Value.Copy(myClass, newIntValue, newStrValue);
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_eql(
            Customer instance, int newInt)
        {
            var ret = instance.With()
                .Eql(m => m.Id, newInt).Copy();
            Assert.Equal(newInt, ret.Id);
            Assert.Equal(instance.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_eql(
            Customer instance, int newInt, string newString)
        {
            var ret = instance.With()
                .Eql(m => m.Id, newInt)
                .Eql(m => m.Name, newString)
                .Copy();
            Assert.Equal(newInt, ret.Id);
            Assert.Equal(newString, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_with_empty_ctor(
            CustomerWithEmptyCtor instance, int newInt, string newString)
        {
            var ret = instance.With()
                .Eql(m => m.Id, newInt)
                .Eql(m => m.Name, newString)
                .Copy();
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
