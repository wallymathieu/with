using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace With.Reflection
{
    static class EnumerableTypeExtensions
    {
        private static ConditionalWeakTable<Type, Type[]> ienumerableTypeParameters = new ConditionalWeakTable<Type, Type[]>();

        internal static Type[] GetIEnumerableTypeParameter(this Type iEnumerableType)
        {
            return ienumerableTypeParameters.WeakMemoize(iEnumerableType, type =>
            {
                if (IsIEnumerableT(iEnumerableType))
                {
                    return iEnumerableType.GetTypeInfo().GetGenericArguments();
                }
                return iEnumerableType.GetTypeInfo().GetInterfaces()
                        .Where(i => IsIEnumerableT(i))
                        .Select(i => i.GetTypeInfo().GetGenericArguments().Single())
                        .ToArray();
            });
        }

        private static bool IsIEnumerableT(Type t)
        {
            return t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }
    }
}
