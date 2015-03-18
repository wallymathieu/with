using System.Linq.Expressions;
using With.Rubyfy;

namespace With.WithPlumbing
{
	class NewArrayFromExpression
	{
		public static object GetValues(NewArrayExpression expression)
		{
			if (expression.NodeType != ExpressionType.NewArrayInit)
				throw new System.Exception();

			return expression.Expressions.Map(e => ExpressionValue.GetExpressionValue(e)).ToA();
		}
	}
}
