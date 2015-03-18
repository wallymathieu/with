using System.Linq.Expressions;
using With.Rubyfy;

namespace With.WithPlumbing
{
	class NewArrayFromExpression
	{
		public static object GetValues(NewArrayExpression expression)
		{
			return expression.Expressions.Map(e => ExpressionValue.GetExpressionValue(e)).ToA();
		}
	}
}
