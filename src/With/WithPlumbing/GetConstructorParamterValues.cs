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
        public object[] GetValues(Object t, IEnumerable<NameAndValue> specifiedValues, FieldOrProperty[] props, ConstructorInfo ctor)
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
                    values[i] = Coerce(param.ParameterType, propertyNameAndValues[param.Name].Value);
                    continue;
                }
                var p = props.SingleOrDefault(prop => prop.Name.Equals(param.Name,
                    StringComparison.InvariantCultureIgnoreCase));
                if (p != null)
                {
                    values[i] = Coerce(param.ParameterType, p.GetValue(t));
                }
                else
                {
                    throw new MissingValueException(param.Name);
                }
            }
            return values;
        }

        public object Coerce(Type parameterType, object v)
        {
            if (v != null && typeof(IEnumerable).IsAssignableFrom(parameterType) && !parameterType.IsAssignableFrom(v.GetType()))
            {
                var typeParam = parameterType.GetIEnumerableTypeParameter();
                if (typeParam != null)
                {
                    return typeof(Enumerable)
                        .GetMethod("Cast")
                        .MakeGenericMethod(typeParam)
                        .Invoke(null, new[] { v });
                }
            }
            return v;
        }
    }
}
