using System.Collections.Generic;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using Xunit;

namespace Tests.With
{
    public class Coercion_should_work
    {
        [Theory, AutoData]
        public void Prefs(
            CustomerWithDifferntTypeOfCollectionsForArguments instance, IEnumerable<string> newValue)
        {
            var ret = CustomerWithDifferntTypeOfCollectionsForArguments._Preferenses.Value.Write(instance, newValue);
            Assert.Equal(newValue, ret.Preferences);
        }
        [Theory, AutoData]
        public void Alpha(
            TypeWithDifferentTypeOfCollectionsForArgumentsAlpha instance, IReadOnlyCollection<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsAlpha._Alpha.Value.Write(instance, newValue);
            Assert.Equal(newValue, ret.Alpha);
        }
        [Theory, AutoData]
        public void Beta(
            TypeWithDifferentTypeOfCollectionsForArgumentsBeta instance, IReadOnlyCollection<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsBeta._Beta.Value.Write(instance, newValue);
            Assert.Equal(newValue, ret.Beta);
        }
        [Theory, AutoData]
        public void Gamma(
            TypeWithDifferentTypeOfCollectionsForArgumentsGamma instance, IReadOnlyList<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsGamma._Gamma.Value.Write(instance, newValue);
            Assert.Equal(newValue, ret.Gamma);
        }
        [Theory, AutoData]
        public void Epsilon(
            TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon instance, IReadOnlyList<string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon._Epsilon.Value.Write(instance, newValue);
            Assert.Equal(newValue, ret.Epsilon);
        }
        [Theory, AutoData]
        public void Zeta(
            TypeWithDifferentTypeOfCollectionsForArgumentsZeta instance, IReadOnlyDictionary<string,string> newValue)
        {
            var ret = TypeWithDifferentTypeOfCollectionsForArgumentsZeta._Zeta.Value.Write(instance, newValue);
            Assert.Equal(newValue, ret.Zeta);
        }
    }
}
