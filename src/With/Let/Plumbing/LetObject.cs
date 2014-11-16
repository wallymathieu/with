using System;
using System.Linq.Expressions;

namespace With.Let.Plumbing
{
	public class LetObject<TObject>
	{
		private readonly TObject Object;
		public LetObject (TObject obj)
		{
			Object = obj;
		}
		public LetMemberOf<TObject,TReturnValue> Member<TReturnValue>( Expression<Func<TObject,TReturnValue>> property)
		{
			return new LetMemberOf<TObject,TReturnValue> (Object, property);
		}
	}
}

