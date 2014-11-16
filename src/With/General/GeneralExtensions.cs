using System;
using System.Collections;

namespace With.General
{
	public static class GeneralExtensions
	{
		public static T[] AsArray<T>(this T t) // is there some way to keep this method from lists, arrays etc?
		{
			return new[] { t };
		}
	}
}

