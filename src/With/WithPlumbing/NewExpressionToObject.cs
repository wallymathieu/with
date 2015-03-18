using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using With.Rubyfy;

namespace With.WithPlumbing
{
	class NewExpressionToObject
	{
		public static object GetNew(NewExpression expression)
		{
			var parameters = expression.Arguments.Map(e => ExpressionValue.GetExpressionValue(e)).ToA();
			return expression.Constructor.Invoke(parameters);
		}

	}
}
