using With;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit.AutoDataAttribute;
using TheoryAttribute = Xunit.Extensions.TheoryAttribute;
using Assert = Xunit.Assert;

namespace Tests
{
	public class Clone_an_instance_into_the_same_type
    {
        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set(
            MyClass myClass, int newValue)
        {
            var ret = myClass.With(m => m.MyProperty, newValue);
            Assert.Equal(newValue, ret.MyProperty);
            Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
        }
        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal(
            MyClass myClass, int newValue)
        {
            var ret = myClass.With(m => m.MyProperty == newValue);
            Assert.Equal(newValue, ret.MyProperty);
            Assert.Equal(myClass.MyProperty2, ret.MyProperty2);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal(
            MyClass myClass, int newIntValue, string newStrValue)
        {
            var ret = myClass.With(m => m.MyProperty == newIntValue && m.MyProperty2 == newStrValue);
            Assert.Equal(newIntValue, ret.MyProperty);
            Assert.Equal(newStrValue, ret.MyProperty2);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal_and_another_propertyvalue(
            MyClass anInstance,MyClass anotherInstance)
        {
            var ret = anotherInstance.With(m => m.MyProperty == anInstance.MyProperty);
            Assert.Equal(anInstance.MyProperty, ret.MyProperty);
            Assert.Equal(anotherInstance.MyProperty2, ret.MyProperty2);
        }
        [Theory, AutoData]
        public void A_class_should_throw_a_decent_exception_when_changing_the_order(
            MyClass anInstance, MyClass anotherInstance)
        {
            Assert.Throws<ShouldBeAnExpressionLeftToRightException>(() => {
                anInstance.With(m => anotherInstance.MyProperty == m.MyProperty);
            });
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_eql(
            MyClass instance, int newInt)
        {
            MyClass ret = instance.With()
                .Eql(m => m.MyProperty, newInt);
            Assert.Equal(newInt, ret.MyProperty);
            Assert.Equal(instance.MyProperty2, ret.MyProperty2);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_eql(
            MyClass instance, int newInt, string newString)
        {
            MyClass ret = instance.With()
                .Eql(m => m.MyProperty, newInt)
                .Eql(m => m.MyProperty2, newString);
            Assert.Equal(newInt, ret.MyProperty);
            Assert.Equal(newString, ret.MyProperty2);
        }
    }
}
