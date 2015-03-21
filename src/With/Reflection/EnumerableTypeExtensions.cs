using System;
using System.Collections.Generic;
using System.Linq;
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
                    return iEnumerableType.GetGenericArguments();
                }
                return iEnumerableType.GetInterfaces()
                        .Where(i => IsIEnumerableT(i))
                        .Select(i => i.GetGenericArguments().Single())
                        .ToArray();
            });
        }

        private static bool IsIEnumerableT(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        internal static DictionaryWrapper ToDictionaryT(this object that)
        {
            return new DictionaryWrapper(that);
        }
    }
}
