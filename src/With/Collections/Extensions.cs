using System;
using System.Collections.Generic;
using System.Linq;
namespace With.Collections
{
	public static class Extensions
	{
        /// <summary>
        /// Returns a new enumerable containing value. Does not modify the existing enumerable.
        /// Should be used when you want immutable enumerables.
        /// </summary>
        /// <returns>A copy of the original IEnumerable with the value appended in the end.</returns>
        /// <param name="enumerable">The IEnumerable that will be the base for the new IEnumerable.</param>
        /// <param name="value">The object to be added to the end of the copy of the IEnumerable.</param>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> enumerable, T value)
		{
            var l = enumerable.ToList();
            l.Add(value);
            return l;
		}

        /// <summary>
        /// Returns a new enumerable containing values. Does not modify the existing enumerable.
        /// Should be used when you want immutable enumerables.
        /// </summary>
        /// <returns>A copy of the original IEnumerable with the appended values in the end.</returns>
        /// <param name="enumerable">The IEnumerable that will be the base for the new IEnumerable.</param>
        /// <param name="values">The objects to be added to the end of the copy of the IEnumerable.</param>
		public static IEnumerable<T> AddRange<T>(this IEnumerable<T> enumerable, IEnumerable<T> values)
		{
            var l = enumerable.ToList();
            l.AddRange(values);
            return l;
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

