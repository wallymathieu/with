using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace With.WithPlumbing
{
    internal class ExpressionWithMemberAccess
    {
        public List<MemberInfo> Members = new List<MemberInfo>();

        private void GetNameFromMemberAccess(MemberExpression member)
        {
            MemberAccess(member);
        }

        public void MemberAccess(Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Parameter:
                    break;
                case ExpressionType.MemberAccess:
                    var memberAccess = (MemberExpression)expr;
                    MemberAccess(memberAccess.Expression);
                    Members.Add(memberAccess.Member);
                    break;
                default:
                    throw new ExpectedButGotException(
                    new[] { ExpressionType.Parameter, ExpressionType.MemberAccess }, expr.NodeType);
            }
        }

        public void Lambda<TRet, TVal>(Expression<Func<TRet, TVal>> expr)
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
