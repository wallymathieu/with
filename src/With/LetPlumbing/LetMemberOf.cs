using System;
using System.Linq.Expressions;

namespace With.LetPlumbing
{
	public class LetMemberOf<TObject,TReturnValueType>
	{
		private readonly TObject Object;
		private readonly Expression<Func<TObject,TReturnValueType>> Property;
		public LetMemberOf (TObject obj, Expression<Func<TObject,TReturnValueType>> property)
		{
			Object = obj;
			Property = property;
		}
		public LetValueBeContext<TObject, TReturnValueType> Be (TReturnValueType value)
		{
			return new LetValueBeContext<TObject, TReturnValueType> (Object, Property, value);
		}
	}
}
