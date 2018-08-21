using Xunit;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;
namespace Tests.With
{
    public class Setting_several_properties_at_once
    {
        public class AClassWithManyProperties
        {
            public AClassWithManyProperties(int myProperty, string myProperty2, string myProperty3, string myProperty4)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
                MyProperty3 = myProperty3;
                MyProperty4 = myProperty4;
            }
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }
            public string MyProperty3 { get; private set; }
            public string MyProperty4 { get; private set; }
        }

        [Theory(Skip = "Not implemented"), AutoData]
        public void should_be_able_to_create_a_clone_using_builder(
            AClassWithManyProperties instance, int newValue, string newValue2, string newValue3, string newValue4)
        {
            /*var ret = instance.With()
                .Eql(m => m.MyProperty, newValue)
                .Eql(m => m.MyProperty2, newValue2)
                .Eql(m => m.MyProperty3, newValue3)
                .Eql(m => m.MyProperty4, newValue4).Copy();
            Assert.Equal(newValue, ret.MyProperty);
            Assert.Equal(newValue2, ret.MyProperty2);
            Assert.Equal(newValue3, ret.MyProperty3);
            Assert.Equal(newValue4, ret.MyProperty4);*/
        }
    }
}
