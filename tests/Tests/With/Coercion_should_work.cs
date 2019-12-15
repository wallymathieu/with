using System.Collections.Generic;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using Xunit;
using With.Lenses;
namespace Tests.With
{
    public class Coercion_should_work
    {
        [Theory, AutoData]
        public void Prefs(
            CustomerWithDifferntTypeOfCollectionsForArguments instance, IEnumerable<string> newValue)
        {
            var ret = CustomerWithDifferntTypeOfCollectionsForArguments._Preferenses.Value.Set(instance, newValue);
            Assert.Equal(newValue, ret.Preferences);
        }
        [Theory, AutoData]
        public void Alpha(
            TypeWithDifferentTypeOfCollectionsForArgumentsAlpha instance, IReadOnlyCollection<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsAlpha._Alpha.Value.Set(instance, newValue);
            Assert.Equal(newValue, ret.Alpha);
        }
        [Theory, AutoData]
        public void Beta(
            TypeWithDifferentTypeOfCollectionsForArgumentsBeta instance, IReadOnlyCollection<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsBeta._Beta.Value.Set(instance, newValue);
            Assert.Equal(newValue, ret.Beta);
        }
        [Theory, AutoData]
        public void Gamma(
            TypeWithDifferentTypeOfCollectionsForArgumentsGamma instance, IReadOnlyList<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsGamma._Gamma.Value.Set(instance, newValue);
            Assert.Equal(newValue, ret.Gamma);
        }
        [Theory, AutoData]
        public void Epsilon(
            TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon instance, IReadOnlyList<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon._Epsilon.Value.Set(instance, newValue);
            Assert.Equal(newValue, ret.Epsilon);
        }
        [Theory, AutoData]
        public void Zeta(
            TypeWithDifferentTypeOfCollectionsForArgumentsZeta instance, IReadOnlyDictionary<string,string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsZeta._Zeta.Value.Set(instance, newValue);
            Assert.Equal(newValue, ret.Zeta);
        }
    }
}
