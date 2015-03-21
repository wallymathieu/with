using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace With.Reflection
{
    internal static class Extensions
    {
        private static MethodInfo EnumerableToList = typeof(Enumerable)
                .GetMethod("ToList");

        /// <summary>
        /// To list of T where T is the IEnumerable T 
        /// </summary>
        internal static IList ToListOfTypeT(this IEnumerable that)
        {
            var t = that.GetType().GetIEnumerableTypeParameter();
            return (IList)EnumerableToList
                .MakeGenericMethod(t)
                .Invoke(null, new[] { that });
        }

        internal static TValue WeakMemoize<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> table, TKey key, Func<TKey, TValue> construct)
             where TKey : class
            where TValue : class
        {
            TValue value;
            if (!table.TryGetValue(key, out value))
            {
                value = construct(key);
                table.Add(key, value);
            }
            return value;
        }

        private static ConditionalWeakTable<Type, FieldOrProperty[]> fieldOrProperties = new ConditionalWeakTable<Type, FieldOrProperty[]>();

        internal static FieldOrProperty[] GetFieldOrProperties(this Type t)
        {
            return fieldOrProperties.WeakMemoize(t,
                type => new FieldOrProperty[0]
                    .Concat(t.GetFields(BindingFlags.Instance | BindingFlags.Public).Select(p => new FieldOrProperty(p)))
                    .Concat(t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new FieldOrProperty(p)))
                    .ToArray());
        }

        private static MethodInfo EnumerableCast = typeof(Enumerable).GetMethod("Cast");

        internal static object Coerce(this object v, Type parameterType)
        {
            if (v != null && typeof(IEnumerable).IsAssignableFrom(parameterType) && !parameterType.IsAssignableFrom(v.GetType()))
            {
                var typeParam = parameterType.GetIEnumerableTypeParameter();
                if (typeParam != null)
                {
                    return EnumerableCast
                        .MakeGenericMethod(typeParam)
                        .Invoke(null, new[] { v });
                }
            }
            return v;
        }
    }
}
