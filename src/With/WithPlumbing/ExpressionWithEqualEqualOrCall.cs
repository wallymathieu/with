using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using With.Rubyfy;

namespace With.WithPlumbing
{
    internal class ExpressionWithEqualEqualOrCall<TRet>
    {
        public ExpressionWithEqualEqualOrCall(Object entity)
        {
            _parsed=new List<NameAndValue>();
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
                            UnaryResultExpression((MethodCallExpression)lambda.Body, lambda);
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

        private void UnaryResultExpression(MethodCallExpression expr, LambdaExpression lambda)
        {
            var memberAccess = expr.Arguments.First();

            _parsed.Add(new NameAndValue
                {
                    Name = MemberAccess((MemberExpression)memberAccess),
                    Value = lambda.Compile().DynamicInvoke(_object)
                });
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
                        new[] {ExpressionType.Equal, ExpressionType.AndAlso, ExpressionType.MemberAccess},
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
                                 Name = MemberAccess((MemberExpression)eq.Left),
                                 Value = ExpressionWithValue(eq.Right)
                             });
        }

        private object ExpressionWithValue(Expression right)
        {
            switch (right.NodeType)
            {
                case ExpressionType.Constant:
                    return ((ConstantExpression)right).Value;
                case ExpressionType.MemberAccess:
                    return GetValue((MemberExpression)right);
                case ExpressionType.Convert:
                    return GetValue((MemberExpression)((UnaryExpression)right).Operand);

                default:
                    throw new ExpectedButGotException(new[] { ExpressionType.Constant, ExpressionType.MemberAccess, ExpressionType.Convert }, 
                        right.NodeType);
            }
        }

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
                throw new ShouldBeAnExpressionLeftToRightException("lambda compile failed, this might be due to wrong order",e);
            }
            return getter();

        }

        private string MemberAccess(MemberExpression member)
        {
            var name = member.Member.Name;
            if (member.Member.DeclaringType != typeof(TRet))
            {
                throw new ShouldBeAnExpressionLeftToRightException("The type indicates that the member expression is invalid");
            }
            return name;
        }
    }
}