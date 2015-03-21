using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace With.WithPlumbing
{
    using Collections;
    using Rubyfy;

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
                var paramValue = expr.Arguments.Drop(1).Map(arg=>ExpressionValue.GetExpressionValue(arg)).ToA();
                value = ApplyOperation.Apply(expr, memberValue, paramValue);
            }
            _parsed.Add(new NameAndValue
            (
                name : memberAccess.Member.Name,
                value : value
            ));
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

    }
}
