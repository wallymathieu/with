using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{
	public interface IStep<T>:IEnumerable<T>
	{
		IStep<T> Step (T step);
		/// <summary>
		/// Determines whether this instance has value without enumerating it.
		/// </summary>
		bool Contain (T value);
	}

}
