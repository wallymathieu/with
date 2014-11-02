using System;
using System.Linq.Expressions;

namespace With.LetPlumbing
{
	public class LetMember<T>
	{
		private readonly Expression<Func<T>> Property;
		public LetMember (Expression<Func<T>> property)
		{
			Property = property;
		}
		public LetValueBeContext<Object, T> Be (T value)
		{
			return new LetValueBeContext<Object, T> (Property,value);
		}
	}
}
