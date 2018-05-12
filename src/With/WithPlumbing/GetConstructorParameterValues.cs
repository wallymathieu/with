using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace With.WithPlumbing
{
    using Reflections;
    internal class GetConstructorParameterValues
    {
        public static object[] GetValues(Object t, IReadOnlyDictionary<string, object> specifiedValues, Internals.FieldOrProperty[] props, ConstructorInfo ctor)
        {
            var ctorParams = ctor.GetParameters();
            var values = new object[ctorParams.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var param = ctorParams[i];
                if (specifiedValues.ContainsKey(param.Name))
                {
                    values[i] = specifiedValues[param.Name].Coerce(param.ParameterType);
                    continue;
                }
                var p = props.SingleOrDefault(prop => prop.Name.Equals(param.Name,
#if NOTCORE
                    StringComparison.InvariantCultureIgnoreCase
#else
                    StringComparison.OrdinalIgnoreCase
#endif
                ));
                if (p != null)
                {
                    values[i] = p.Value(t).Coerce(param.ParameterType);
                }
                else
                {
                    throw new MissingValueException(param.Name);
                }
            }
            return values;
        }
    }
}
