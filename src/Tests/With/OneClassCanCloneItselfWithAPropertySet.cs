using With;
using Xunit;
using TestAttribute = Xunit.FactAttribute;
namespace Tests
{
    public class OneClassCanCloneItselfWithAPropertySet
    {
        public class MyClass
        {
            public MyClass(int myProperty, string myProperty2)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
            }
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }
        }

        [Test]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set()
        {
            var ret = new MyClass(1, "2").With(m => m.MyProperty, 3);
            Assert.Equal(ret.MyProperty, 3);
            Assert.Equal(ret.MyProperty2, "2");
        }
        [Test]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal()
        {
            var ret = new MyClass(1, "2").With(m => m.MyProperty == 3);
            Assert.Equal(ret.MyProperty, 3);
            Assert.Equal(ret.MyProperty2, "2");
        }
        [Test]
        public void A_class_should_be_able_to_create_a_clone_with_two_property_set_using_equal_equal()
        {
            var ret = new MyClass(1, "2").With(m => m.MyProperty == 3 && m.MyProperty2 == "3");
            Assert.Equal(ret.MyProperty, 3);
            Assert.Equal(ret.MyProperty2, "3");
        }

        [Test]
        public void A_class_should_be_able_to_create_a_clone_with_a_property_set_using_equal_equal_and_another_propertyvalue()
        {
            var t = new MyClass(3, "3");
            var ret = new MyClass(1, "2").With(m => m.MyProperty == t.MyProperty);
            Assert.Equal(ret.MyProperty, 3);
            Assert.Equal(ret.MyProperty2, "2");
        }
        [Test]
        public void A_class_should_throw_a_decent_exception_when_changing_the_order()
        {
            var t = new { MyProperty = 3 };
            var mc = new MyClass(1, "2");
            Assert.Throws<ShouldBeAnExpressionLeftToRightException>(() => mc.With(m => t.MyProperty == m.MyProperty));
        }
    }
}
