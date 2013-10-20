using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace With.Plumbing
{
    internal class ExpressionWithEqualEqual<TRet>
    {
        public ExpressionWithEqualEqual()
        {
            _parsed=new List<NameAndValue>();
        }

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
                        case ExpressionType.AndAlso:
                            BinaryExpression((BinaryExpression)lambda.Body);
                            break;
                        default:
                            throw new Exception(lambda.Body.NodeType.ToString());
                    }
                    break;
                default:
                    throw new Exception(expr.NodeType.ToString());
            }
        }

        private void BinaryExpression(BinaryExpression expr)
        {
            switch (expr.Left.NodeType)
            {
                case ExpressionType.AndAlso:
                case ExpressionType.Equal:
                    {
                        BinaryExpression((BinaryExpression)expr.Left);
                        switch (expr.Right.NodeType)
                        {
                            case ExpressionType.AndAlso:
                            case ExpressionType.Equal:
                                BinaryExpression((BinaryExpression)expr.Right);
                                break;
                            default:
                                throw new Exception(expr.Right.NodeType.ToString());
                        }
                    }
                    break;
                case ExpressionType.MemberAccess:
                    {
                        BinaryExpressionWithMemberAccess(expr);
                    }
                    break;
                default:
                    throw new Exception(expr.Left.NodeType.ToString());
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
                    throw new Exception(right.NodeType.ToString() + Environment.NewLine + right.GetType().FullName);
            }
        }

        protected internal static object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

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