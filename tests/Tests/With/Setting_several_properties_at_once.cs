using System;
using With;
using Xunit;
using AutoDataAttribute = Ploeh.AutoFixture.Xunit2.AutoDataAttribute;
using With.Lenses;
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

        readonly Lazy<DataLens<AClassWithManyProperties, (int, string, string, string)>> copyExpr = LazyT.Create(() =>
            LensBuilder<AClassWithManyProperties>
                        .Of<int, string>((m, v1, v2) => m.MyProperty == v1 && m.MyProperty2 == v2)
                        .And<string>((m, v1) => m.MyProperty3 == v1)
                        .And(m => m.MyProperty4)
                        .Build());
        readonly Lazy<DataLens<AClassWithManyProperties, (int, string, string, string)>> copyExpr2 = LazyT.Create(() =>
            LensBuilder<AClassWithManyProperties>
                        .Of<int>((m, v1) => m.MyProperty == v1)
                        .And<string>((m, v1) => m.MyProperty2 == v1)
                        .And<string>((m, v1) => m.MyProperty3 == v1)
                        .And(m => m.MyProperty4)
                        .Build());

        [Theory, AutoData]
        public void should_be_able_to_create_a_clone_using_builder1(
                AClassWithManyProperties instance, int newValue, string newValue2, string newValue3, string newValue4)
        {
            var ret = copyExpr.Value.Write(instance, (newValue, newValue2, newValue3, newValue4));
            Assert.Equal(newValue, ret.MyProperty);
            Assert.Equal(newValue2, ret.MyProperty2);
            Assert.Equal(newValue3, ret.MyProperty3);
            Assert.Equal(newValue4, ret.MyProperty4);
        }
        [Theory, AutoData]
        public void should_be_able_to_create_a_clone_using_builder2(
                AClassWithManyProperties instance, int newValue, string newValue2, string newValue3, string newValue4)
        {
            var ret = copyExpr2.Value.Write(instance, (newValue, newValue2, newValue3, newValue4));
            Assert.Equal(newValue, ret.MyProperty);
            Assert.Equal(newValue2, ret.MyProperty2);
            Assert.Equal(newValue3, ret.MyProperty3);
            Assert.Equal(newValue4, ret.MyProperty4);
        }
    }
}
