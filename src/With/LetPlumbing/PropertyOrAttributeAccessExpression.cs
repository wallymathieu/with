using System;
using System.Reflection;
using System.Linq.Expressions;

namespace With.LetPlumbing
{
	internal class PropertyOrFieldAccessExpression
	{

		public PropertyOrFieldAccessExpression ()
		{
			
		}

		public MemberInfo Member {
			get;
			private set;
		}

		public void Lambda(LambdaExpression lambdaExpression)
		{
			Expression expressionToCheck = lambdaExpression;

			bool done = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
				case ExpressionType.Convert:
					expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
					break;
				case ExpressionType.Lambda:
					expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
					break;
				case ExpressionType.MemberAccess:
					var memberExpression = ((MemberExpression)expressionToCheck);

					if (memberExpression.Expression != null) {
						if (memberExpression.Expression.NodeType != ExpressionType.Parameter &&
						    memberExpression.Expression.NodeType != ExpressionType.Convert) {
							throw new ArgumentException (String.Format ("Expression '{0}' must resolve to top-level member.", 
								lambdaExpression), "lambdaExpression");
						}
					}

					Member = memberExpression.Member;
					return;
					//return member;
				default:
					done = true;
					break;
				}
			}
			throw new ArgumentException("Failed!");
		}
	}
}

