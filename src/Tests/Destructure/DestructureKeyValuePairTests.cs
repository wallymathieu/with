using Ploeh.AutoFixture.Xunit2;
using System;
using Xunit;
using With.Destructure;
using System.Collections.Generic;
using With;
namespace Tests
{
    public class DestructureKeyValuePairTests
    {
        [Theory, AutoData]
        public void Second_variable(
            KeyValuePair<int, int> instance)
        {
            Assert.Equal(instance.Value, instance.Let((a, b) => b));
        }

        [Theory, AutoData]
        public void First_variable(
            KeyValuePair<int, int> instance)
        {
            Assert.Equal(instance.Key, instance.Let((a, b) => a));
        }

        [Theory, AutoData]
        public void Action(
            KeyValuePair<int, int> instance)
        {
            int? item = null;
            instance.Let((a, b) => { item = a; });
            Assert.Equal(instance.Key, item);
        }
    }
}

