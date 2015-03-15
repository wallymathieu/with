using System;
using System.Collections.Generic;
using With.Rubyfy;
namespace With.Collections
{
	public static class Extensions
	{
		public static IEnumerable<T> Add<T>(this IEnumerable<T> self, T value)
		{
			var array = self.ToA();
			var retval = new T[array.Length + 1];
			array.CopyTo(retval, 0);
			retval[array.Length] = value;
			return retval;
		}

		public static IEnumerable<T> AddRange<T>(this IEnumerable<T> self, IEnumerable<T> value)
		{
			var array = self.ToA();
			var toBeAdded = value.ToA();
			var retval = new T[array.Length + toBeAdded.Length];
			array.CopyTo(retval, 0);
			toBeAdded.CopyTo(retval, array.Length);
			return retval;
		}

		private static bool ReturnsTrue<T>(T element) { return true; }
		public static T Next<T>(this IList<T> that, int index, Func<T, bool> filter = null, Func<int, T> valueWhenOutOfRange = null)
		{
			if (null == filter) filter = ReturnsTrue;
			for (int i = index + 1; i < that.Count; i++)
			{
				var item = that[i];
				if (filter(item))
					return item;
			}
			if (valueWhenOutOfRange != null)
			{
				return valueWhenOutOfRange(index);
			}
			throw new OutOfRangeException();
		}

		public static T Previous<T>(this IList<T> that, int index, Func<T, bool> filter = null, Func<int,T> valueWhenOutOfRange = null)
		{
			if (null == filter) filter = ReturnsTrue;
			for (int i = index - 1; 0 <= i; i--)
			{
				var item = that[i];
				if (filter(item))
					return item;
			}
			if (valueWhenOutOfRange != null)
			{
				return valueWhenOutOfRange(index);
			}
			throw new OutOfRangeException();
		}
	}
}

