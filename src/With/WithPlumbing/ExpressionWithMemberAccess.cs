using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using With.Internals;

namespace With.WithPlumbing
{
    internal class ExpressionWithMemberAccess
    {
        public List<FieldOrProperty> Members = new List<FieldOrProperty>();
        public void Lambda<TRet, TVal>(Expression<Func<TRet, TVal>> expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Lambda:
                    switch (expr.Body.NodeType)
                    {
                        case ExpressionType.MemberAccess:
                            Members.AddRange(Expressions.ExpressionWithMemberAccess((MemberExpression)expr.Body));
                            break;
                        case ExpressionType.Convert:
                            Members.AddRange(Expressions.ExpressionWithMemberAccess((MemberExpression)((UnaryExpression)expr.Body).Operand));
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
