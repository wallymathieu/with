using System;
using System.Linq.Expressions;

namespace With.LetPlumbing
{
	/// <summary>
	/// Sets the value of a property or a field and then resets that value when this class is disposed.
	/// </summary>
	public class LetValueBeContext<TObj,T> : IDisposable
	{
		private readonly PropertyOrField _property;
		private readonly object _oldvalue;

		public LetValueBeContext(TObj target, Expression<Func<TObj,T>> property, T value)
		{
			var member = new PropertyOrFieldAccessExpression ().Tap (e => e.Lambda (property)).Member;
			_property = new PropertyOrField(target,member);
			_oldvalue = _property.GetMemberValue();
			_property.SetMemberValue(value);
		}

		public LetValueBeContext(Expression<Func<T>> property, T value)
		{
			var member = new PropertyOrFieldAccessExpression ().Tap (e => e.Lambda (property)).Member;
			_property = new PropertyOrField(null, member);
			_oldvalue = _property.GetMemberValue();
			_property.SetMemberValue(value);
		}

		public void Dispose()
		{
			_property.SetMemberValue(_oldvalue);
		}
	}
}

