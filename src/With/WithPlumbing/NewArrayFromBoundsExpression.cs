using System.Linq.Expressions;
using With.Rubyfy;

namespace With.WithPlumbing
{
	class NewArrayFromBoundsExpression
	{
		internal static object GetValue(NewArrayExpression expression)
		{
			var bounds = expression.Expressions.Map(e => ExpressionValue.GetExpressionValue(e)).ToA();
			return expression.Type.GetConstructors().First().Invoke(bounds);
		}
	}
}
