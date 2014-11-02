using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace With.WithPlumbing
{
	public class ValuesForConstructor<T>
	{
		private readonly Object parent;
		private readonly PropertyInfo[] props;
		private readonly ConstructorInfo[] ctors;
		private readonly ConstructorInfo ctor;
		private readonly IList<NameAndValue> values;

		public ValuesForConstructor (Object parent)
		{
			this.parent = parent;
			this.props = typeof(T).GetProperties();
			this.ctors = typeof(T).GetConstructors().ToArray();
			this.ctor = ctors.Single();
			values = new List<NameAndValue> ();
		}
		public ValuesForConstructor<T> Eql<TValue> (Expression<Func<T, TValue>> expr, TValue val)
		{
			var memberAccess = new ExpressionWithMemberAccess<T, TValue>();
			memberAccess.Lambda(expr);
			var propertyName = memberAccess.MemberName;
			values.Add(new NameAndValue (propertyName, val));
			return this;
		}

		public T To()
		{
			return (T)ctor.Invoke( new GetConstructorParamterValues().GetValues(parent, values, props, ctor));
		}

		public static implicit operator T(ValuesForConstructor<T> d)
		{
			return d.To ();
		}
	}
}
