using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace With
{
    public static class WithExtensions
    {
        public static TRet With<TRet>(this Object t, params Object[] parameters)
        {
            var props = t.GetType().GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
            var ctorParams = ctor.GetParameters();
            var values = new object[ctorParams.Length];
            for (int i = 0; i < values.Length - parameters.Length; i++)
            {
                var param = ctorParams[i];
                var p = props.SingleOrDefault(prop => prop.Name.Equals(param.Name, StringComparison.InvariantCultureIgnoreCase));
                if (p != null)
                {
                    values[i] = p.GetValue(t, null);
                }
                else
                {
                    throw new MissingValueException();
                }
            }

            parameters.CopyTo(values, values.Length - parameters.Length);
            return (TRet)ctor.Invoke(values);
        }

        public static TRet With<TRet, TVal>(this TRet t, Expression<Func<TRet, TVal>> expr, TVal val)
        {
            var props = typeof(TRet).GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
            var ctorParams = ctor.GetParameters();
            var values = new object[ctorParams.Length];
            var propertyName = GetPropertyName(expr);

            for (int i = 0; i < values.Length; i++)
            {
                var param = ctorParams[i];
                if (param.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    values[i] = val;
                    continue;
                }
                var p = props.SingleOrDefault(prop => prop.Name.Equals(param.Name, StringComparison.InvariantCultureIgnoreCase));
                if (p != null)
                {
                    values[i] = p.GetValue(t, null);
                }
                else
                {
                    throw new MissingValueException();
                }
            }

            return (TRet)ctor.Invoke(values);
        }

        private static string GetPropertyName<TRet, TVal>(Expression<Func<TRet, TVal>> expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Lambda:
                    switch (expr.Body.NodeType)
                    {
                        case ExpressionType.MemberAccess:
                            return GetNameFromMemberAccess<TRet>((MemberExpression)expr.Body);
                        default:
                            throw new Exception(expr.Body.NodeType.ToString());
                    }
                default:
                    throw new Exception(expr.NodeType.ToString());
            }
        }

        public static TRet With<TRet>(this TRet t, Expression<Func<TRet, bool>> expr)
        {
            var props = typeof(TRet).GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
            var ctorParams = ctor.GetParameters();
            var values = new object[ctorParams.Length];
            var propertyNameAndValues = GetPropertyNameAndValue<TRet>(expr).ToArray().ToDictionary(nv=>nv.Name,
                StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < values.Length; i++)
            {
                var param = ctorParams[i];
                if (propertyNameAndValues.ContainsKey(param.Name))
                {
                    values[i] = propertyNameAndValues[param.Name].Value;
                    continue;
                }
                var p = props.SingleOrDefault(prop => prop.Name.Equals(param.Name, StringComparison.InvariantCultureIgnoreCase));
                if (p != null)
                {
                    values[i] = p.GetValue(t, null);
                }
                else
                {
                    throw new MissingValueException();
                }
            }

            return (TRet)ctor.Invoke(values);
        }
        private class NameAndValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        private static IEnumerable<NameAndValue> GetPropertyNameAndValue<TRet>(Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Lambda:
                    var lambda = ((LambdaExpression)expr);
                    switch (lambda.Body.NodeType)
                    {
                        case ExpressionType.Equal:
                        case ExpressionType.AndAlso:
                            var retval = new List<NameAndValue>();
                            BinaryExpression<TRet>((BinaryExpression)lambda.Body,retval);
                            return retval;
                            break;
                        default:
                            throw new Exception(lambda.Body.NodeType.ToString());
                    }
                    break;
                default:
                    throw new Exception(expr.NodeType.ToString());
            }
        }

        private static void BinaryExpression<TRet>(BinaryExpression expr, IList<NameAndValue> retval)
        {
            switch (expr.Left.NodeType)
            {
                case ExpressionType.AndAlso:
                case ExpressionType.Equal:
                    {
                        BinaryExpression<TRet>((BinaryExpression)expr.Left, retval);
                        switch (expr.Right.NodeType)
                        {
                            case ExpressionType.AndAlso:
                            case ExpressionType.Equal:
                                BinaryExpression<TRet>((BinaryExpression)expr.Right,retval);
                                break;
                            default:
                                throw new Exception(expr.Right.NodeType.ToString());
                        }
                    }
                    break;
                case ExpressionType.MemberAccess:
                    {
                        retval.Add(BinaryExpressionWithMemberAccess<TRet>(expr));
                    }
                    break;
                default:
                    throw new Exception(expr.Left.NodeType.ToString());
            }


        }

        private static NameAndValue BinaryExpressionWithMemberAccess<TRet>(BinaryExpression eq)
        {
            var retval = new NameAndValue
            {
                Name = GetNameFromMemberAccess<TRet>((MemberExpression)eq.Left),
                Value = GetValueFromExpression(eq.Right)
            };
            return retval;
        }

        private static string GetNameFromMemberAccess<TRet>(MemberExpression member)
        {
            var name = member.Member.Name;
            if (member.Member.DeclaringType != typeof(TRet))
            {
                throw new ShouldBeAnExpressionLeftToRightException("The type indicates that the member expression is invalid");
            }
            return name;
        }

        private static object GetValueFromExpression(Expression right)
        {
            switch (right.NodeType)
            {
                case ExpressionType.Constant:
                    return ((ConstantExpression)right).Value;
                case ExpressionType.MemberAccess:
                    return GetValue((MemberExpression)right);

                default:
                    throw new Exception(right.NodeType.ToString() + Environment.NewLine + right.GetType().FullName);
                    break;
            }
        }
        private static object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }


    }
}
