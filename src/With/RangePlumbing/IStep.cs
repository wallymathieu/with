using System;
using System.Collections;
using System.Collections.Generic;

namespace With.RangePlumbing
{
	public interface IStep<T>:IEnumerable<T>
	{
		IStep<T> Step (T step);
	}

}
