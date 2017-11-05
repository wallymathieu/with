using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using Ploeh.AutoFixture.Xunit2;

namespace Tests.With.TestData
{
    public class ImmutableAutoDataAttribute : AutoDataAttribute
    {
        public ImmutableAutoDataAttribute()
        {
            this.Fixture.ResidueCollectors.Add(new ImmutableListRelay());
        }
        class ImmutableListRelay : ISpecimenBuilder
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
                    && type.GetGenericTypeDefinition().Equals(typeof(IImmutableList<>)))
                {
                    var typeArguments = type.GetGenericArguments();
                    var t = typeof(ImmutableList<>).MakeGenericType(typeArguments);
                    var empty = t.GetField("Empty").GetValue(null);

                    return t.GetMethod("AddRange").Invoke(empty, BindingFlags.Instance, null, new object[] {
                        context.Resolve(typeof(IEnumerable<>).MakeGenericType(typeArguments))
                    }, CultureInfo.CurrentCulture);
                }
                return new NoSpecimen(request);
            }
        }
    }
}
