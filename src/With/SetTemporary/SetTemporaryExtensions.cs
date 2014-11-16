using System;
using With.Let.Plumbing;
using System.Linq.Expressions;

namespace With.SetTemporary
{
	public static class SetTemporaryExtensions
	{
		public static LetValueBeContext<TObject, TReturnValue> SetTemporary<TObject, TReturnValue>(this TObject obj, Expression<Func<TObject, TReturnValue>> property, TReturnValue value)
		{
			return new LetValueBeContext<TObject, TReturnValue>(obj, property, value);
		}
	}
}

