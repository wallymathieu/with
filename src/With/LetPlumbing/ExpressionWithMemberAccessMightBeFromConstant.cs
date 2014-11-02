using System;
using System.Linq.Expressions;

namespace With.LetPlumbing
{
	internal class ExpressionWithMemberAccessMightBeFromConstant:ExpressionWithMemberAccess
	{
		public object ConstantValue {
			get;
			private set;
		}

		protected override void MemberAccess (MemberExpression memberExpression)
		{
			if (memberExpression.Expression != null) 
			{
				ExpectParameterOrMemberAccess (memberExpression.Expression);
			}
			base.MemberAccess (memberExpression);
		}

		protected virtual void ExpectParameterOrMemberAccess (Expression expression)
		{
			switch (expression.NodeType) {
			case ExpressionType.Parameter:
				Parameter ((ParameterExpression)expression);
				break;
			case ExpressionType.MemberAccess:
				MemberAccess ((MemberExpression)expression);
				break;
			case ExpressionType.Constant:
				Constant ((ConstantExpression)expression);
				break;
			default:
				throw new ExpectedButGotException(
					new[] {ExpressionType.Parameter, ExpressionType.MemberAccess}, expression.NodeType);
			}
		}


		protected void Parameter (ParameterExpression parameterExpression)
		{
		}

		protected void Constant (ConstantExpression constantExpression)
		{
			if (ConstantValue != null) {
				throw new Exception ("Expected only one constant expression!");
			}
			ConstantValue = constantExpression.Value;
		}
	}
}

