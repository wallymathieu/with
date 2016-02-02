using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace With.Reflection
{
    using Linq;
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

        internal static FieldOrProperty[] GetFieldsOrProperties(this Type t)
        {
            return fieldOrProperties.WeakMemoize(t,
                type => new FieldOrProperty[0]
                .Concat(GetPublicFields(type).Select(p => new FieldOrProperty(p)))
                .Concat(GetPublicProperties(type).Select(p => new FieldOrProperty(p)))
                .ToArray()
            );
        }

        private static ConditionalWeakTable<Type, FieldOrPropertyOrGetMethod[]> fieldOrPropertyOrMethods = new ConditionalWeakTable<Type, FieldOrPropertyOrGetMethod[]>();

        internal static FieldOrPropertyOrGetMethod[] GetFieldsOrPropertiesOrGetMethods(this Type t, TypeOfFIelds typeOfFIelds)
        {
            return fieldOrPropertyOrMethods.WeakMemoize(t,
                type => new FieldOrPropertyOrGetMethod[0]
                .Concat(typeOfFIelds.HasFlag(TypeOfFIelds.Fields)
                    ? GetPublicFields(type).Select(p => new FieldOrPropertyOrGetMethod(p))
                    : new FieldOrPropertyOrGetMethod[0])
                .Concat(typeOfFIelds.HasFlag(TypeOfFIelds.Properties)
                    ? GetPublicProperties(type).Select(p => new FieldOrPropertyOrGetMethod(p))
                    : new FieldOrPropertyOrGetMethod[0])
                .Concat(typeOfFIelds.HasFlag(TypeOfFIelds.Methods)
                    ? GetPublicGetMethods(type).Select(p => new FieldOrPropertyOrGetMethod(p))
                    : new FieldOrPropertyOrGetMethod[0])
                .ToArray()
            );
        }

        private static IEnumerable<PropertyInfo> GetPublicProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private static Regex _getNotUnderscore = new Regex("^get[^_]", RegexOptions.IgnoreCase);
        private static Regex _hashcode = new Regex("^gethashcode", RegexOptions.IgnoreCase);
        private static IEnumerable<MethodInfo> GetPublicGetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.DeclaringType != typeof(Object)
                    && !m.Name.Match(_hashcode).Success
                    && m.Name.Match(_getNotUnderscore).Success);
        }

        private static IEnumerable<FieldInfo> GetPublicFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        }


        private static MethodInfo EnumerableCast = typeof(Enumerable).GetMethod("Cast");

        internal static object Coerce(this object v, Type parameterType)
        {
            if (v != null
                && typeof(IEnumerable).IsAssignableFrom(parameterType) 
                && !parameterType.IsAssignableFrom(v.GetType()))
            {
                var typeParam = parameterType.GetIEnumerableTypeParameter();
                if (typeParam != null)
                {
                    return EnumerableCast
                        .MakeGenericMethod(typeParam)
                        .Invoke(null, new[] { v });
                }
            }
#if DEBUG
            if (v != null
                && !parameterType.IsAssignableFrom(v.GetType()))
            {
                throw new Exception(string.Format("parameter type {0} is not assignable from {1}",
                    parameterType.Name,
                    v.GetType().Name));
            }
#endif
            return v;
        }
    }
}
