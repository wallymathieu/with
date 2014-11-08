using System;
using With;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit.AutoDataAttribute;
using TheoryAttribute = Xunit.Extensions.TheoryAttribute;
using Assert = Xunit.Assert;
namespace Tests
{
	public class BuildingWith
	{
        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set(MyClass instance, int newInt)
		{
            MyClass ret = instance.With()
				.Eql(m => m.MyProperty, newInt);
			Assert.Equal(newInt, ret.MyProperty);
			Assert.Equal(instance.MyProperty2, ret.MyProperty2);
		}

        [Theory, AutoData]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal(MyClass instance, int newInt,string newString)
		{
            MyClass ret = instance.With()
				.Eql(m => m.MyProperty, newInt)
				.Eql(m => m.MyProperty2, newString);
			Assert.Equal(newInt, ret.MyProperty);
			Assert.Equal(newString, ret.MyProperty2);
		}
	}
}

