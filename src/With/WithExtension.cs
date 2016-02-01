using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.WithPlumbing;
using NameAndValue = System.Collections.Generic.KeyValuePair<string, object>;

namespace With
{
    public static class WithExtensions
    {
        public static ValuesForConstructor<T> With<T>(this T t)
        {
            return new ValuesForConstructor<T>(t);
        }

        public static T With<T, TVal>(this T t, Expression<Func<T, TVal>> expr, TVal val)
        {
            var memberAccess = new ExpressionWithMemberAccess();
            memberAccess.Lambda<T, TVal>(expr);

            return CreateInstanceFromValues.Create<T,T>(t,new[] { GetNameAndValue.Get(t, memberAccess.Members, val) });
        }

        public static T With<T>(this T t, IDictionary<string, object> parameters)
        {
            return CreateInstanceFromValues.Create<T,T>(t,parameters);
        }

        public static T With<T>(this T t, Expression<Func<T, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);

            return CreateInstanceFromValues.Create<T,T>(t, eqeq.Parsed.ToArray());
        }

        public static T With<T, TVal>(this T t, Expression<Func<T, TVal>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            return CreateInstanceFromValues.Create<T, T>(t, propertyNameAndValues);
        }

        public static T With<T>(this T t, Expression<Action<T>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            return CreateInstanceFromValues.Create<T, T>(t,propertyNameAndValues);
        }

        public static ValuesForConstructor<TRet> As<TRet>(this Object t)
        {
            return new ValuesForConstructor<TRet>(t);
        }

        public static TRet As<TRet>(this Object t, IDictionary<string, object> parameters)
        {
            return CreateInstanceFromValues.Create<TRet>(t, parameters.Select(v => new NameAndValue(v.Key, v.Value)));
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, object>> expr, object val)
        {
            var memberAccess = new ExpressionWithMemberAccess();
            memberAccess.Lambda<TRet, object>(expr);
            return CreateInstanceFromValues.Create<TRet>(t,new[] { GetNameAndValue.Get(t, memberAccess.Members, val) });
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<TRet>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            return CreateInstanceFromValues.Create<TRet>(t, propertyNameAndValues);
        }
    }
}
