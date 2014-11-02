using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using With.WithPlumbing;

namespace With
{
    public static class WithExtensions
    {
		public static With<T> With<T>(this T t)
		{
			return new WithPlumbing.With<T>(t);
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

        public static TRet As<TRet>(this Object t, params Object[] parameters)
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
                    throw new MissingValueException(param.Name);
                }
            }

            parameters.CopyTo(values, values.Length - parameters.Length);
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
