using System;
using System.Collections.Generic;
using System.Linq;

namespace With.ReadonlyEnumerable
{
	public static class Extensions
	{
		public static void Add<T>(this IEnumerable<T> enumerable, T value)
		{
			throw new NotSupportedException();
		}

		public static void AddRange<T>(this IEnumerable<T> enumerable, IEnumerable<T> values)
		{
			throw new NotSupportedException();
		}

		public static void Remove<T>(this IEnumerable<T> enumerable, T value)
		{
			throw new NotSupportedException();
		}
	}
}
