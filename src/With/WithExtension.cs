﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.Reflection;
using With.WithPlumbing;

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
            var props = typeof(T).GetFieldOrProperties();
            var memberAccess = new ExpressionWithMemberAccess<T, TVal>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;

			var ctor = new FindMatchingCtor<T>().Get();

			var values = new GetConstructorParamterValues().GetValues(t, new[] { new NameAndValue(propertyName, val) }, props, ctor);

            return (T)ctor.Invoke(values);
        }

        public static T With<T>(this T t, IDictionary<string, object> parameters)
        {
            var props = typeof(T).GetFieldOrProperties();
            var ctors = typeof(T).GetConstructors().ToArray();
            var ctor = ctors.Single();

			var values = new GetConstructorParamterValues().GetValues(t, parameters.Select(v => new NameAndValue(v.Key, v.Value)), props, ctor);

            return (T)ctor.Invoke(values);
        }

        public static T With<T>(this T t, Expression<Func<T, bool>> expr)
        {
			var props = typeof(T).GetFieldOrProperties();
			var ctor = new FindMatchingCtor<T>().Get();
			var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();

			var values = new GetConstructorParamterValues().GetValues(t, propertyNameAndValues, props, ctor);

            return (T)ctor.Invoke(values);
        }

        public static T With<T,TVal>(this T t, Expression<Func<T, TVal>> expr)
        {
			var props = typeof(T).GetFieldOrProperties();
			var ctor = new FindMatchingCtor<T>().Get();
			var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();

            var values = new GetConstructorParamterValues().GetValues(t, propertyNameAndValues, props, ctor);

            return (T)ctor.Invoke(values);
        }

		public static T With<T>(this T t, Expression<Action<T>> expr)
		{
			var props = typeof(T).GetFieldOrProperties();
			var ctor = new FindMatchingCtor<T>().Get();
			var eqeq = new ExpressionWithEqualEqualOrCall<T>(t);
			eqeq.Lambda(expr);
			var propertyNameAndValues = eqeq.Parsed.ToArray();

			var values = new GetConstructorParamterValues().GetValues(t, propertyNameAndValues, props, ctor);

			return (T)ctor.Invoke(values);
		}

		public static ValuesForConstructor<TRet> As<TRet>(this Object t)
		{
			return new ValuesForConstructor<TRet>(t);
		}

        public static TRet As<TRet>(this Object t, Object first, params Object[] parameters)
        {
			var allParameters = new Object[parameters.Length+1];
			allParameters[0]= first;
			parameters.CopyTo (allParameters, 1);

			var values = new GetParameterValuesUsingOrdinal ().GetValues (t, typeof(TRet), allParameters);
			var ctor = new FindMatchingCtor<TRet>().Get();
           	return (TRet)ctor.Invoke(values);
        }


        public static TRet As<TRet>(this Object t, IDictionary<string, object> parameters)
        {
            var props = t.GetType().GetFieldOrProperties();
			var ctor = new FindMatchingCtor<TRet>().Get();
			var values = new GetConstructorParamterValues().GetValues(t, parameters.Select(v => new NameAndValue(v.Key, v.Value)), props, ctor);

            return (TRet)ctor.Invoke(values);
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, object>> expr, object val)
        {
            var props = typeof(TRet).GetFieldOrProperties();
			var ctor = new FindMatchingCtor<TRet>().Get();
			var memberAccess = new ExpressionWithMemberAccess<TRet, object>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;

			var values = new GetConstructorParamterValues().GetValues(t, new[] { new NameAndValue(propertyName, val) }, props, ctor);

            return (TRet)ctor.Invoke(values);
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<TRet>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();

            var props = t.GetType().GetFieldOrProperties();
			var ctor = new FindMatchingCtor<TRet>().Get();
			var values = new GetConstructorParamterValues().GetValues(t, propertyNameAndValues, props, ctor);
            return (TRet)ctor.Invoke(values);
        }
    }
}
