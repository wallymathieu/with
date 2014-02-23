using NUnit.Framework;
using With;

namespace Tests
{
    [TestFixture]
    public class SwitchCase
    {
        public class MyClass
        {

        }

        public class MyClass2
        {

        }
        public class MyClass3
        {

        }
        [Test]
        public void Single_case()
        {
            var instance = new MyClass();

            var result = Switch.On(instance)
                .Case((MyClass c) => 1);
            Assert.That(result.Value(), Is.EqualTo(1));
        }

        [Test]
        public void Multi_case()
        {
            var instance = new MyClass();

            var result = Switch.On(instance)
                .Case((MyClass c) => 1)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3);
            Assert.That(result.Value(), Is.EqualTo(1));
        }

        [Test]
        public void Multi_case_with_a_differnt_order()
        {
            var instance = new MyClass();

            var result = Switch.On(instance)
                .Case((MyClass2 c) => 2)
                .Case((MyClass3 c) => 3)
                .Case((MyClass c) => 1);
            Assert.That(result.Value(), Is.EqualTo(1));
        }

    }
}
