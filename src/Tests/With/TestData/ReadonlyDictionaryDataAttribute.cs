using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Globalization;
using System;
using Ploeh.AutoFixture.Kernel;

namespace Tests.With.TestData
{
    class ReadonlyDictionaryDataAttribute : AutoDataAttribute
    {
        public ReadonlyDictionaryDataAttribute()
        {
            this.Fixture.ResidueCollectors.Add(new ReadOnlyDictionaryRelay());
        }
        class ReadOnlyDictionaryRelay : ISpecimenBuilder
        {
            public object Create(object request, ISpecimenContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                var type = request as Type;

                if (type != null
                    && type.IsGenericType
                    && type.GetGenericTypeDefinition().Equals(typeof(IReadOnlyDictionary<,>)))
                {
                    var typeArguments = type.GetGenericArguments();
                    return context.Resolve(typeof(ReadOnlyDictionary<,>).MakeGenericType(typeArguments));
                }
                return new NoSpecimen(request);
            }
        }
    }
}
