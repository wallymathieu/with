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
                case ExpressionType.NewArrayBounds:
                case ExpressionType.NewArrayInit:
                case ExpressionType.New:
                    return GetValue(expression);
                case ExpressionType.Convert:
                    return GetValue(((UnaryExpression)expression).Operand);
                default:
                    throw new ExpectedButGotException(new[] { ExpressionType.Constant, ExpressionType.MemberAccess, ExpressionType.NewArrayBounds, ExpressionType.NewArrayInit, ExpressionType.Convert, ExpressionType.New },
                        expression.NodeType);
            }
        }
        /// <summary>
        /// NOTE: Lots of time spent here!
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private static object GetValue(Expression member)
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
