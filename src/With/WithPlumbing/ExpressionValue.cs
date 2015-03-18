using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace With.WithPlumbing
{
	class ExpressionValue
	{
		public static object GetExpressionValue(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Constant:
					return ((ConstantExpression)expression).Value;
				case ExpressionType.MemberAccess:
					return MemberExpressionValue.GetValue((MemberExpression)expression);
				case ExpressionType.NewArrayBounds:
					return NewArrayFromBoundsExpression.GetValue((NewArrayExpression)expression);
				case ExpressionType.NewArrayInit:
					return NewArrayFromExpression.GetValues((NewArrayExpression)expression);
				case ExpressionType.Convert:
					return MemberExpressionValue.GetValue((MemberExpression)((UnaryExpression)expression).Operand);
				case ExpressionType.New:
					return NewExpressionToObject.GetNew((NewExpression)expression);
				default:
					throw new ExpectedButGotException(new[] { ExpressionType.Constant, ExpressionType.MemberAccess, ExpressionType.NewArrayBounds, ExpressionType.NewArrayInit, ExpressionType.Convert, ExpressionType.New },
						expression.NodeType);
			}
		}
	}
}
