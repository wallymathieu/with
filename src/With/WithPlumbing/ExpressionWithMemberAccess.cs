using System;
using System.Linq.Expressions;

namespace With.WithPlumbing
{
    internal class ExpressionWithMemberAccess<TRet, TVal>
    {
        public string MemberName { get; private set; }

        private void GetNameFromMemberAccess(MemberExpression member)
        {
            MemberName = member.Member.Name;
        }

        public void Lambda(Expression<Func<TRet, TVal>> expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Lambda:
                    switch (expr.Body.NodeType)
                    {
                        case ExpressionType.MemberAccess:
                            GetNameFromMemberAccess((MemberExpression)expr.Body);
                            break;
                        case ExpressionType.Convert:
                            GetNameFromMemberAccess((MemberExpression)((UnaryExpression)expr.Body).Operand);
                            break;
                        default:
                            throw new ExpectedButGotException(
                                new[] { ExpressionType.MemberAccess, ExpressionType.Convert }, expr.Body.NodeType);
                    }
                    break;
                default:
                    throw new ExpectedButGotException(new[] { ExpressionType.Lambda },
                                                      expr.NodeType);
            }
        }
    }
}