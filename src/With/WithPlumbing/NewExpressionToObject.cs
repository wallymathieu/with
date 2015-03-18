using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using With.Rubyfy;

namespace With.WithPlumbing
{
	class NewExpressionToObject
	{
		private static readonly GetConstructorParamterValues _getCtorParamValues = new GetConstructorParamterValues();
		public static object GetNew(NewExpression expression)
		{
			var ctor = expression.Constructor;
			var ctorParams = ctor.GetParameters();
			var parameters = expression.Arguments.Map(e => ExpressionValue.GetExpressionValue(e))
				.Map((v, i) => _getCtorParamValues.Coerce(ctorParams[i].ParameterType, v)).ToA();

			return ctor.Invoke(parameters);
		}

	}
}
