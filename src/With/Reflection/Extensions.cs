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
			return t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new FieldOrProperty(p))
				.Concat(t.GetFields(BindingFlags.Instance | BindingFlags.Public).Select(p => new FieldOrProperty(p)))
				.ToArray();
		}
	}
}
