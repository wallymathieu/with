using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace With.Reflection
{
	internal static class Extensions
	{
		public static FieldOrProperty[] GetFieldOrProperties(this Type t)
		{
			return
				new FieldOrProperty[0]
				.Concat(t.GetFields(BindingFlags.Instance | BindingFlags.Public).Select(p => new FieldOrProperty(p)))
				.Concat(t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new FieldOrProperty(p)))
				.ToArray();
		}

		public static Type[] GetIEnumerableTypeParameter(this Type iEnumerableType)
		{
			if (IsIEnumerableT(iEnumerableType))
			{
				return iEnumerableType.GetGenericArguments();
			}
			return iEnumerableType.GetInterfaces()
					.Where(i => IsIEnumerableT(i))
					.Select(i => i.GetGenericArguments().Single())
					.ToArray();
		}
		private static bool IsIEnumerableT(Type t)
		{
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }
	}
}
