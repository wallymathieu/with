using With;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class Clone_an_instance_into_the_same_type
    {
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
            var ret = myClass.PrepareWith<Customer,int>((m,v) => m.Id == v).Copy(newValue);
            Assert.Equal(newValue, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal(
            Customer myClass, int newIntValue, string newStrValue)
        {
            var ret = myClass.PrepareWith<Customer,int,string>((m,v1,v2) => m.Id == v1 && m.Name==v2).Copy(newIntValue,newStrValue);
            Assert.Equal(newIntValue, ret.Id);
            Assert.Equal(newStrValue, ret.Name);
        }

        /*[Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal_and_another_propertyvalue(
            Customer anInstance, Customer anotherInstance)
        {
            var ret = anotherInstance.With(m => m.Id == anInstance.Id);
            Assert.Equal(anInstance.Id, ret.Id);
            Assert.Equal(anotherInstance.Name, ret.Name);
        }*/
        /*[Theory, AutoData]
        public void A_class_should_throw_a_decent_exception_when_changing_the_order(
            Customer anInstance, Customer anotherInstance)
        {
            Assert.Throws<ShouldBeAnExpressionLeftToRightException>(() =>
            {
                anInstance.With(m => anotherInstance.Id == m.Id);
            });
        }*/

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_eql(
            Customer instance, int newInt)
        {
            Customer ret = instance.With()
                .Eql(m => m.Id, newInt);
            Assert.Equal(newInt, ret.Id);
            Assert.Equal(instance.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_eql(
            Customer instance, int newInt, string newString)
        {
            Customer ret = instance.With()
                .Eql(m => m.Id, newInt)
                .Eql(m => m.Name, newString);
            Assert.Equal(newInt, ret.Id);
            Assert.Equal(newString, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_with_empty_ctor(
            CustomerWithEmptyCtor instance, int newInt, string newString)
        {
            Customer ret = instance.With()
                .Eql(m => m.Id, newInt)
                .Eql(m => m.Name, newString);
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
