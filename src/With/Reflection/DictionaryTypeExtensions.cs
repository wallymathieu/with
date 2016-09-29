using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace With.Reflection
{
    static class DictionaryTypeExtensions
    {
        private static ConditionalWeakTable<Type, Type[]> dictionaryTypeParameters = new ConditionalWeakTable<Type, Type[]>();

        internal static Type[] GetIDictionaryTypeParameters(this Type iDictionaryType)
        {
            return dictionaryTypeParameters.WeakMemoize(iDictionaryType, type =>
            {
                if (IsExactlyDictionaryType(iDictionaryType))
                {
                    return iDictionaryType.GetTypeInfo().GetGenericArguments();
                }
                var t = iDictionaryType.GetTypeInfo().GetInterfaces()
                        .Where(i => IsExactlyDictionaryType(i))
                        .FirstOrDefault();
                return t != null ? t.GetTypeInfo().GetGenericArguments() : new Type[0];
            });
        }
        internal static bool IsDictionaryType(this Type t)
        {
            return GetIDictionaryTypeParameters(t).Any();
        }

        private static bool IsExactlyDictionaryType(Type t)
        {
            return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>);
        }

        private static ConditionalWeakTable<Type, ConstructorInfo> dictionaryTypeCtor = new ConditionalWeakTable<Type, ConstructorInfo>();

        internal static DictionaryAdapter ToDictionaryOfTypeT(this object that)
        {
            Type type = that.GetType();
            var dic = dictionaryTypeCtor.WeakMemoize(type,t=>
                typeof(Dictionary<,>)
                    .MakeGenericType(t.GetIDictionaryTypeParameters())
                    .GetTypeInfo().GetConstructor(new Type[0])
            ).Invoke(new object[0]);

            return new DictionaryAdapter((IDictionary)that, (IDictionary)dic);
        }
    }
}
