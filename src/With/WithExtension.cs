using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.Reflection;
using With.WithPlumbing;
using With.Collections;
using NameAndValue = System.Collections.Generic.KeyValuePair<string,object>;

namespace With
{
    public static class WithExtensions
    {
        public static ValuesForConstructor<T> With<T>(this T t)
        {
            return new WithPlumbing.ValuesForConstructor<T>(t);
        }

        public static T With<T, TVal>(this T t, Expression<Func<T, TVal>> expr, TVal val)
        {
            var memberAccess = new ExpressionWithMemberAccess<T, TVal>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;

            var createInstance = new CreateInstanceFromValues<T> ();
            return  createInstance.Create(t,new[] { NameAndValues.Create(propertyName, val) });
        }

        public static T With<T>(this T t, IDictionary<string, object> parameters)
        {
            var createInstance = new CreateInstanceFromValues<T> ();
            return createInstance.Create(t,parameters);
        }

        public static T With<T>(this T t, Expression<Func<T, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);

            var createInstance = new CreateInstanceFromValues<T> ();
            return createInstance.Create(t, eqeq.Parsed.ToArray());
        }

        public static T With<T, TVal>(this T t, Expression<Func<T, TVal>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            var createInstance = new CreateInstanceFromValues<T> ();
            return createInstance.Create(t, propertyNameAndValues);
        }

        public static T With<T>(this T t, Expression<Action<T>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            var createInstance = new CreateInstanceFromValues<T>();
            return createInstance.Create(t,propertyNameAndValues);
        }

        public static ValuesForConstructor<TRet> As<TRet>(this Object t)
        {
            return new ValuesForConstructor<TRet>(t);
        }

        public static TRet As<TRet>(this Object t, IDictionary<string, object> parameters)
        {
            var createInstance = new CreateInstanceFromValues<TRet>(t.GetType());
            return createInstance.Create(t, parameters.Select(v => new NameAndValue(v.Key, v.Value)));
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, object>> expr, object val)
        {
            var memberAccess = new ExpressionWithMemberAccess<TRet, object>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;
            var createInstance = new CreateInstanceFromValues<TRet>(t.GetType());
            return createInstance.Create(t,new[] { new NameAndValue(propertyName, val) });
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<TRet>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            var createInstance = new CreateInstanceFromValues<TRet>(t.GetType());
            return createInstance.Create(t, propertyNameAndValues);
        }
    }
}
