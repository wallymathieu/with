using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using With.Reflection;
using System.Collections;

namespace With.WithPlumbing
{
    internal class GetConstructorParamterValues
    {
        public static object[] GetValues(Object t, IEnumerable<NameAndValue> specifiedValues, FieldOrProperty[] props, ConstructorInfo ctor)
        {
            var ctorParams = ctor.GetParameters();
            var values = new object[ctorParams.Length];
            var propertyNameAndValues = specifiedValues.ToDictionary(nv => nv.Name,
                StringComparer.InvariantCultureIgnoreCase);
            for (int i = 0; i < values.Length; i++)
            {
                var param = ctorParams[i];
                if (propertyNameAndValues.ContainsKey(param.Name))
                {
                    values[i] = propertyNameAndValues[param.Name].Value.Coerce(param.ParameterType);
                    continue;
                }
                var p = props.SingleOrDefault(prop => prop.Name.Equals(param.Name,
                    StringComparison.InvariantCultureIgnoreCase));
                if (p != null)
                {
                    values[i] = p.GetValue(t).Coerce(param.ParameterType);
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
