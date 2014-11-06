﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using With.WithPlumbing;

namespace With
{
    public static class WithExtensions
    {
		public static ValuesForConstructor<T> With<T>(this T t)
		{
			return new WithPlumbing.ValuesForConstructor<T>(t);
		}

        public static TRet With<TRet, TVal>(this TRet t, Expression<Func<TRet, TVal>> expr, TVal val)
        {
            var props = typeof(TRet).GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
            var memberAccess = new ExpressionWithMemberAccess<TRet, TVal>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;

			var values = new GetConstructorParamterValues().GetValues(t, new[] { new NameAndValue(propertyName, val) }, props, ctor);

            return (TRet)ctor.Invoke(values);
        }

        public static TRet With<TRet>(this TRet t, IDictionary<string, object> parameters)
        {
            var props = typeof(TRet).GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();

			var values = new GetConstructorParamterValues().GetValues(t, parameters.Select(v => new NameAndValue(v.Key, v.Value)), props, ctor);

            return (TRet)ctor.Invoke(values);
        }

        public static TRet With<TRet>(this TRet t, Expression<Func<TRet, bool>> expr)
        {
            var props = typeof(TRet).GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
            var eqeq = new ExpressionWithEqualEqual<TRet>();
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();

			var values = new GetConstructorParamterValues().GetValues(t, propertyNameAndValues, props, ctor);

            return (TRet)ctor.Invoke(values);
        }
		public static ValuesForConstructor<TRet> As<TRet>(this Object t)
		{
			return new WithPlumbing.ValuesForConstructor<TRet> (t);
		}

        public static TRet As<TRet>(this Object t, Object first, params Object[] parameters)
        {
			var allParameters = new Object[parameters.Length+1];
			allParameters[0]= first;
			parameters.CopyTo (allParameters, 1);

			var values = new GetParameterValuesUsingOrdinal ().GetValues (t, typeof(TRet), allParameters);
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
           	return (TRet)ctor.Invoke(values);
        }


        public static TRet As<TRet>(this Object t, IDictionary<string, object> parameters)
        {
            var props = t.GetType().GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();

			var values = new GetConstructorParamterValues().GetValues(t, parameters.Select(v => new NameAndValue(v.Key, v.Value)), props, ctor);

            return (TRet)ctor.Invoke(values);
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, object>> expr, object val)
        {
            var props = typeof(TRet).GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
            var memberAccess = new ExpressionWithMemberAccess<TRet, object>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;

			var values = new GetConstructorParamterValues().GetValues(t, new[] { new NameAndValue(propertyName, val) }, props, ctor);

            return (TRet)ctor.Invoke(values);
        }

        public static TRet As<TRet>(this Object t, Expression<Func<TRet, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqual<TRet>();
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();

            var props = t.GetType().GetProperties();
            var ctors = typeof(TRet).GetConstructors().ToArray();
            var ctor = ctors.Single();
			var values = new GetConstructorParamterValues().GetValues(t, propertyNameAndValues, props, ctor);

            return (TRet)ctor.Invoke(values);
        }
    }
}
