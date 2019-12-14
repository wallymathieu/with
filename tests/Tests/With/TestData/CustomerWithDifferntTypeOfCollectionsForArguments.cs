using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using With;
using With.Lenses;

namespace Tests.With.TestData
{
    public class TypeWithDifferentTypeOfCollectionsForArgumentsAlpha
    {
        public TypeWithDifferentTypeOfCollectionsForArgumentsAlpha(IEnumerable<string> alpha)
        {
            Alpha = new ReadOnlyCollection<string>(alpha.ToList());
        }
        ///<summary> IReadOnlyCollection -> IEnumerable </summary>
        public static Lazy<DataLens<TypeWithDifferentTypeOfCollectionsForArgumentsAlpha, IReadOnlyCollection<string>>> _Alpha =
             LazyT.Create(() => LensBuilder<TypeWithDifferentTypeOfCollectionsForArgumentsAlpha>.Of(c => c.Alpha).Build());
        public IReadOnlyCollection<string> Alpha { get; }
    }
    public class TypeWithDifferentTypeOfCollectionsForArgumentsBeta
    {
        public TypeWithDifferentTypeOfCollectionsForArgumentsBeta(IList<string> beta)
        {
            Beta = new ReadOnlyCollection<string>(beta);
        }

        ///<summary> IReadOnlyCollection -> IList </summary>
        public static Lazy<DataLens<TypeWithDifferentTypeOfCollectionsForArgumentsBeta, IReadOnlyCollection<string>>> _Beta =
             LazyT.Create(() => LensBuilder<TypeWithDifferentTypeOfCollectionsForArgumentsBeta>.Of(c => c.Beta).Build());
        public IReadOnlyCollection<string> Beta { get; }
    }
    public class TypeWithDifferentTypeOfCollectionsForArgumentsGamma
    {
        public TypeWithDifferentTypeOfCollectionsForArgumentsGamma(IList<string> gamma)
        {
            Gamma = new ReadOnlyCollection<string>(gamma);
        }

        ///<summary> IReadOnlyList -> IList </summary>
        public static Lazy<DataLens<TypeWithDifferentTypeOfCollectionsForArgumentsGamma, IReadOnlyList<string>>> _Gamma =
            LazyT.Create(() => LensBuilder<TypeWithDifferentTypeOfCollectionsForArgumentsGamma>.Of(c => c.Gamma).Build());
        public IReadOnlyList<string> Gamma { get; }
    }
    public class TypeWithDifferentTypeOfCollectionsForArgumentsDelta
    {
        public TypeWithDifferentTypeOfCollectionsForArgumentsDelta(List<string> delta)
        {
            Delta = delta;
        }

        ///<summary> IEnumerable -> List </summary>
        public static Lazy<DataLens<TypeWithDifferentTypeOfCollectionsForArgumentsDelta, IEnumerable<string>>> _Delta =
            LazyT.Create(() => LensBuilder<TypeWithDifferentTypeOfCollectionsForArgumentsDelta>.Of(c => c.Delta).Build());
        public IEnumerable<string> Delta { get; }
    }
    public class TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon
    {
        public TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon(ReadOnlyCollection<string> epsilon)
        {
            Epsilon = epsilon;
        }

        ///<summary> IReadOnlyCollection -> ReadOnlyCollection </summary>
        public static Lazy<DataLens<TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon, IReadOnlyCollection<string>>> _Epsilon =
            LazyT.Create(() => LensBuilder<TypeWithDifferentTypeOfCollectionsForArgumentsEpsilon>.Of(c => c.Epsilon).Build());
        public IReadOnlyCollection<string> Epsilon { get; }
    }
    public class TypeWithDifferentTypeOfCollectionsForArgumentsZeta
    {
        public TypeWithDifferentTypeOfCollectionsForArgumentsZeta(IDictionary<string, string> zeta)
        {
            Zeta = new ReadOnlyDictionary<string, string>(zeta);
        }

        ///<summary> IReadOnlyDictionary -> IDictionary </summary>
        public static Lazy<DataLens<TypeWithDifferentTypeOfCollectionsForArgumentsZeta, IReadOnlyDictionary<string, string>>> _Zeta =
            LazyT.Create(() => LensBuilder<TypeWithDifferentTypeOfCollectionsForArgumentsZeta>.Of(c => c.Zeta).Build());
        public IReadOnlyDictionary<string, string> Zeta { get; }
    }
    public class CustomerWithDifferntTypeOfCollectionsForArguments : Customer
    {
        public CustomerWithDifferntTypeOfCollectionsForArguments(int id, string name,
            IList<string> preferences)
            : base(id, name, preferences)
        {
        }
        ///<summary> IEnumerable -> IList </summary>
        public static new Lazy<DataLens<CustomerWithDifferntTypeOfCollectionsForArguments, IEnumerable<string>>> _Preferenses =
             LazyT.Create(() => LensBuilder<CustomerWithDifferntTypeOfCollectionsForArguments>.Of(c => c.Preferences).Build());
    }
}
