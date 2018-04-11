﻿using System;
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
        private static readonly MethodInfo EnumerableToList = typeof (Enumerable)
                .GetTypeInfo ().GetMethod ("ToList");

        /// <summary>
        /// To list of T where T is the IEnumerable T 
        /// </summary>
        internal static IList ToListOfTypeT (this IEnumerable that)
        {
            var t = that.GetType ().GetIEnumerableTypeParameter ();
            return (IList)EnumerableToList
                .MakeGenericMethod (t)
                .Invoke (null, new [] { that });
        }
        private static object lockObj = new object ();
        internal static TValue WeakMemoize<TKey, TValue> (this ConditionalWeakTable<TKey, TValue> table, TKey key, Func<TKey, TValue> construct)
            where TKey : class
            where TValue : class
        {
            TValue value;
            lock (lockObj) {
                if (!table.TryGetValue (key, out value)) {
                    value = construct (key);
                    table.Add (key, value);
                }
            }
            return value;
        }

        private static ConditionalWeakTable<Type, FieldOrProperty []> fieldOrProperties = new ConditionalWeakTable<Type, FieldOrProperty []> ();

        internal static FieldOrProperty [] GetFieldsOrProperties (this Type t)
        {
            return fieldOrProperties.WeakMemoize (t,
                type => new FieldOrProperty [0]
                .Concat (GetPublicFields (type).Select (p => new FieldOrProperty (p)))
                .Concat (GetPublicProperties (type).Select (p => new FieldOrProperty (p)))
                .ToArray ()
            );
        }

        private static ConditionalWeakTable<Type, FieldOrPropertyOrGetMethod []> fieldOrPropertyOrMethods = new ConditionalWeakTable<Type, FieldOrPropertyOrGetMethod []> ();

        private static IEnumerable<PropertyInfo> GetPublicProperties (Type type)
        {
            return type
                .GetTypeInfo ().GetProperties (BindingFlags.Public | BindingFlags.Instance);
        }

        private static Regex _getNotUnderscore = new Regex ("^get[^_]", RegexOptions.IgnoreCase);
        private static Regex _hashcode = new Regex ("^gethashcode", RegexOptions.IgnoreCase);
        private static IEnumerable<MethodInfo> GetPublicGetMethods (Type type)
        {
            return type
                .GetTypeInfo ().GetMethods (BindingFlags.Public | BindingFlags.Instance)
                .Where (m => m.DeclaringType != typeof (Object)
                     && !m.Name.Match (_hashcode).Success
                     && m.Name.Match (_getNotUnderscore).Success);
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
                var typeParam = parameterType.GetIEnumerableTypeParameter ();
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
