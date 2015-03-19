using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace With.WithPlumbing
{
	internal class ExpressionWithEqualEqualOrCall<TRet>
	{
		public ExpressionWithEqualEqualOrCall(Object entity)
		{
			_parsed = new List<NameAndValue>();
			_object = entity;
		}
		private object _object;
		private List<NameAndValue> _parsed;
		public IEnumerable<NameAndValue> Parsed
		{
			get { return _parsed; }
		}

		public void Lambda(Expression expr)
		{
			switch (expr.NodeType)
			{
				case ExpressionType.Lambda:
					var lambda = ((LambdaExpression)expr);
					switch (lambda.Body.NodeType)
					{
						case ExpressionType.Equal:
						case ExpressionType.AndAlso://TODO: Fix, this is a bit sloppy
							BinaryExpressionAndOrEqualOrMemberAccess((BinaryExpression)lambda.Body);
							break;
						case ExpressionType.Call:
							UnaryResultExpression(lambda);
							break;
						default:
							throw new ExpectedButGotException(
								new[] { ExpressionType.Equal, ExpressionType.AndAlso, ExpressionType.Call },
								lambda.Body.NodeType);
					}
					break;
				default:
					throw new ExpectedButGotException(new[] { ExpressionType.Lambda }, expr.NodeType);
			}
		}

		private void UnaryResultExpression(LambdaExpression lambda)
		{
			var unaryCall = new ExpressionUnaryCall(_object);
			unaryCall.Lambda(lambda);
			_parsed.AddRange(unaryCall.Parsed);
		}

		/// <summary>
		/// TODO: Refactor
		/// </summary>
		/// <param name="expr"></param>
		private void BinaryExpressionAndOrEqualOrMemberAccess(BinaryExpression expr)
		{
			switch (expr.Left.NodeType)
			{
				case ExpressionType.AndAlso:
				case ExpressionType.Equal://TODO: Fix, this is a bit sloppy
					BinaryExpressionAndOrEqual(expr);
					break;
				case ExpressionType.MemberAccess:
					BinaryExpressionWithMemberAccess(expr);
					break;
				default:
					throw new ExpectedButGotException(
						new[] { ExpressionType.Equal, ExpressionType.AndAlso, ExpressionType.MemberAccess },
						expr.Left.NodeType);
			}
		}

		private void BinaryExpressionAndOrEqual(BinaryExpression expr)
		{
			BinaryExpressionAndOrEqualOrMemberAccess((BinaryExpression)expr.Left);
			switch (expr.Right.NodeType)
			{
				case ExpressionType.AndAlso:
				case ExpressionType.Equal://TODO: Fix, this is a bit sloppy
					BinaryExpressionAndOrEqualOrMemberAccess((BinaryExpression)expr.Right);
					break;
				default:
					throw new ExpectedButGotException(new[] { ExpressionType.Equal, ExpressionType.AndAlso },
													  expr.Right.NodeType);
			}
		}

		private void BinaryExpressionWithMemberAccess(BinaryExpression eq)
		{
			_parsed.Add(new NameAndValue
			{
				Name = MemberName((MemberExpression)eq.Left),
				Value = ExpressionValue.GetExpressionValue(eq.Right)
			});
		}

		private string MemberName(MemberExpression member)
		{
			return member.Member.Name;
		}
	}
}