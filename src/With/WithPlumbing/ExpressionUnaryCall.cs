using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using With.Collections;
using With.Rubyfy;

namespace With.WithPlumbing
{
	internal class ExpressionUnaryCall
	{
		private object _object;
		private List<NameAndValue> _parsed;
		public IEnumerable<NameAndValue> Parsed
		{
			get { return _parsed; }
		}
		public ExpressionUnaryCall(Object entity)
		{
			_parsed = new List<NameAndValue>();
			_object = entity;
		}


		public void Lambda(Expression expr)
		{
			switch (expr.NodeType)
			{
				case ExpressionType.Lambda:
					var lambda = ((LambdaExpression)expr);
					switch (lambda.Body.NodeType)
					{
						case ExpressionType.Call:
							UnaryResultExpression((MethodCallExpression)lambda.Body, lambda);
							break;
						default:
							throw new ExpectedButGotException(
								new[] { ExpressionType.Call },
								lambda.Body.NodeType);
					}
					break;
				default:
					throw new ExpectedButGotException(new[] { ExpressionType.Lambda }, expr.NodeType);
			}
		}

		private void UnaryResultExpression(MethodCallExpression expr, LambdaExpression lambda)
		{
			var memberAccess = (MemberExpression)expr.Arguments.First();
			object value;
			if (lambda.ReturnType != null && lambda.ReturnType != typeof(void))
			{
				value = lambda.Compile().DynamicInvoke(_object);
			}
			else
			{
				var memberValue = GetMemberValue(memberAccess);
				var paramValue = ExpressionValue.GetExpressionValue(expr.Arguments.Drop(1).First());
				value = ApplyOperation(expr, memberValue, paramValue);
			}
			_parsed.Add(new NameAndValue
			{
				Name = memberAccess.Member.Name,
				Value = value
			});
		}

		private object ApplyOperation(MethodCallExpression expr, object memberValue, object paramValue)
		{
			object value;
			switch (expr.Method.Name)
			{
				case "Add":
					value = Add(memberValue, paramValue);
					break;
				case "AddRange":
					value = AddRange(memberValue, (IEnumerable)paramValue);
					break;
				case "Remove":
					value = Remove(memberValue, paramValue);
					break;
				default:
					throw new ExpectedButGotException(new[] { "Add", "AddRange", "Remove" },
						expr.Method.Name);
			}

			return value;
		}

		private object GetMemberValue(MemberExpression memberAccess)
		{
			var m = memberAccess.Member;
			switch (m.MemberType)
			{
				case MemberTypes.Field:
					return _object.GetType().GetField(m.Name).GetValue(_object);
				case MemberTypes.Property:
					return _object.GetType().GetProperty(m.Name).GetValue(_object, null);
				default:
					throw new ExpectedButGotException(new[] { MemberTypes.Field, MemberTypes.Property },
						m.MemberType);
			}
		}

		private object Add(object v, object p)
		{
			var l = ((IEnumerable)v).ToListT();
			l.Add(p);
			return l;
		}

		private object AddRange(object v, IEnumerable p)
		{
			var l = ((IEnumerable)v).ToListT();
			foreach (var item in p)
			{
				l.Add(item);
			}
			return l;
		}
		private object Remove(object v, object p)
		{
			var l = ((IEnumerable)v).ToListT();
			l.Remove(p);
			return l;
		}
	}
}
