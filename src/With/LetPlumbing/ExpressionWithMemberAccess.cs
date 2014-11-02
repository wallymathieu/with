using System;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace With.LetPlumbing
{
	internal class ExpressionWithMemberAccess
	{

		public ExpressionWithMemberAccess ()
		{
			Members = new List<MemberInfo> ();
		}
		public IList<MemberInfo> Members;
		public MemberInfo Member 
		{
			get { return Members.Last(); }
		}

		public void Lambda(LambdaExpression lambdaExpression)
		{
			var body = lambdaExpression.Body;
			ExpectMemberAccess (body);
		}

		protected virtual void MemberAccess(MemberExpression memberExpression)
		{
			Members.Add(memberExpression.Member);
		}

		protected virtual void ExpectMemberAccess (Expression body)
		{
			if (body.NodeType == ExpressionType.MemberAccess)
			{
				MemberAccess((MemberExpression)body);
			} else 
			{
				throw new ExpectedButGotException(
					new[] {ExpressionType.MemberAccess}, body.NodeType);
			}
		}

	}
}

