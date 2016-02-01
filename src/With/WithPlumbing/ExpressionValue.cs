using System;
using System.Linq;
using System.Linq.Expressions;

namespace With.WithPlumbing
{
    class ExpressionValue
    {
        private static ExpressionType[] expected = new[] {
            ExpressionType.Constant, ExpressionType.Convert,
            ExpressionType.MemberAccess, ExpressionType.NewArrayBounds, ExpressionType.NewArrayInit,  ExpressionType.New,
            ExpressionType.Call, ExpressionType.Invoke,
        };

        public static object GetExpressionValue(Expression expression)
        {
            if (!expected.Contains(expression.NodeType))
            {
                throw new ExpectedButGotException(expected, expression.NodeType);
            }
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                    return ((ConstantExpression)expression).Value;
                case ExpressionType.Convert:
                    return GetValue(((UnaryExpression)expression).Operand);
                default:
                    return GetValue(expression);
            }

        }
        /// <summary>
        /// NOTE: Lots of time spent here!
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private static object GetValue(Expression member)
        {
            var getterLambda = Expression.Lambda<Func<object>>(
                Expression.Convert(member, typeof(object))
                );
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
