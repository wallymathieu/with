using Ploeh.AutoFixture.Xunit;
using System;
using Xunit;
using Xunit.Extensions;
using With.Destructure;

namespace Tests.Destructure
{
    class DestructureTupleTests
    {
        [Theory, AutoData]
        public void Second_variable(
            Tuple<int, int> instance)
        {
            Assert.Equal(instance.Item2, instance.Let((a, b) => b));
        }

        [Theory, AutoData]
        public void First_variable(
            Tuple<int, int> instance)
        {
            Assert.Equal(instance.Item1, instance.Let((a, b) => a));
        }

        [Theory, AutoData]
        public void Action(
            Tuple<int, int> instance)
        {
            int? item=null;
            instance.Let((a, b) => {item=a;});
            Assert.Equal(instance.Item1, item);
        }
    }
}
