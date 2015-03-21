using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
                    return iDictionaryType.GetGenericArguments();
                }
                var t = iDictionaryType.GetInterfaces()
                        .Where(i => IsExactlyDictionaryType(i))
                        .FirstOrDefault();
                return t != null ? t.GetGenericArguments() : new Type[0];
            });
        }
        internal static bool IsDictionaryType(this Type t)
        {
            return GetIDictionaryTypeParameters(t).Any();
        }

        private static bool IsExactlyDictionaryType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>);
        }

    }
}
