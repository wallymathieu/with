using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.FSharp.Core;
using With.Internals;

namespace With.Reflections
{
    internal static class Extensions
    {
        private static readonly FSharpFunc<Type, FieldOrProperty[]> FieldOrProperties=Reflection.WeakMemoize(new Func<Type, FieldOrProperty[]>( type => new FieldOrProperty [0]
            .Concat(GetPublicFields(type).Select(FieldOrProperty.Create))
            .Concat(GetPublicProperties(type).Select(FieldOrProperty.Create))
            .ToArray()).ToFSharpFunc());
        internal static FieldOrProperty [] GetFieldsOrProperties (this Type t) => FieldOrProperties.Invoke(t);

        private static IEnumerable<PropertyInfo> GetPublicProperties (Type type)
        {
            return type
                .GetTypeInfo ().GetProperties (BindingFlags.Public | BindingFlags.Instance);
        }

        private static IEnumerable<FieldInfo> GetPublicFields (Type type)
        {
            return type
                    .GetTypeInfo ().GetFields (BindingFlags.Public | BindingFlags.Instance);
        }


        private static MethodInfo EnumerableCast = typeof (Enumerable)
                    .GetTypeInfo ().GetMethod ("Cast")
            ;

        internal static object Coerce (this object v, Type parameterType)
        {
            if (v != null
                && typeof (IEnumerable).GetTypeInfo ().IsAssignableFrom (parameterType)
                && !parameterType.GetTypeInfo ().IsAssignableFrom (v.GetType ())
            ) {
                var typeParam = Reflection.GetIEnumerableTypeParameter.Invoke( parameterType);
                if (typeParam != null) {
                    return EnumerableCast
                        .MakeGenericMethod (typeParam)
                        .Invoke (null, new [] { v });
                }
            }
#if DEBUG
            if (v != null
                && !parameterType.GetTypeInfo ().IsAssignableFrom (v.GetType ())
            ) {
                throw new Exception (string.Format ("parameter type {0} is not assignable from {1}",
                    parameterType.Name,
                    v.GetType ().Name));
            }
#endif
            return v;
        }
    }
}
