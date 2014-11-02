using System;
using System.Reflection;
using System.Linq.Expressions;

namespace With.LetPlumbing
{
	internal class ExpressionWithMemberAccess
	{

		public ExpressionWithMemberAccess ()
		{
			
		}

		public MemberInfo Member {
			get;
			private set;
		}

		public void Lambda(LambdaExpression lambdaExpression)
		{
			var body = lambdaExpression.Body;
			ExpectMemberAccess (body);
		}

		private void MemberAccess(MemberExpression memberExpression)
		{
			Member = memberExpression.Member;
		}

		private void ExpectMemberAccess (Expression body)
		{
			if (body.NodeType == ExpressionType.MemberAccess)
			{
				MemberAccess((MemberExpression)body);
			} else 
			{
				throw new ExpectedButGotException(
					new[] {ExpressionType.MemberAccess, ExpressionType.Convert}, body.NodeType);
			}
		}
	}
}

