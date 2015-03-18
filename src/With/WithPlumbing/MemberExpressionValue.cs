using System;
using System.Linq.Expressions;

namespace With.WithPlumbing
{
	class MemberExpressionValue
	{
		/// <summary>
		/// NOTE: Lots of time spent here!
		/// </summary>
		/// <param name="member"></param>
		/// <returns></returns>
		protected internal static object GetValue(MemberExpression member)
		{
			var objectMember = Expression.Convert(member, typeof(object));

			var getterLambda = Expression.Lambda<Func<object>>(objectMember);
			Func<object> getter;
			try
			{
				getter = getterLambda.Compile();
			}
			catch (InvalidOperationException e)
			{
				// ugly, should probably do something to be able to do any direction
				throw new ShouldBeAnExpressionLeftToRightException("lambda compile failed, this might be due to wrong order", e);
			}
			return getter();
		}
	}
}
