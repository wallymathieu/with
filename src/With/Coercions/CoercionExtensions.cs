using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.WithPlumbing;
using NameAndValue = System.Collections.Generic.KeyValuePair<string, object>;

namespace With.Coercions
{
    /// <summary>
    /// Coercion extensions: Coerce an object to another object
    /// </summary>
    public static class CoercionExtensions
    {
        /// <summary>
        /// Starts constructing another object based on the base object.
        /// </summary>
        /// <returns>A builder for getting a new object based on the existing object.</returns>
        /// <param name="t">The object to use as a template.</param>
        /// <typeparam name="T">The type of the object that should be returned.</typeparam>
        /// <typeparam name="TFrom">The type of the base object that should be used as a template.</typeparam>
        public static ValuesForConstructor<T> As<TFrom,T>(this TFrom t) where T:TFrom
        {
            return new ValuesForConstructor<T>(t);
        }

        /// <summary>
        /// Coerce an object into another object based on the base object.
        /// </summary>
        /// <returns>A new object based on the existing object.</returns>
        /// <param name="t">The object to use as a template.</param>
        /// <param name="parameters">The constructor parameters to override.</param>
        /// <typeparam name="T">The type of the object that should be returned.</typeparam>
        /// <typeparam name="TFrom">The type of the base object that should be used as a template.</typeparam>
        public static T As<TFrom,T>(this TFrom t, IDictionary<string, object> parameters) where T:TFrom
        {
            return CreateInstanceFromValues.Create<T>(t, parameters.ToArray());
        }
        /// <summary>
        /// Coerce an object into another object based on the base object.
        /// </summary>
        /// <returns>A new object based on the existing object.</returns>
        /// <param name="t">The object to use as a template.</param>
        /// <param name="expr">What property or field to be different</param>
        /// <param name="val">The value for the property expected to be changed</param>
        /// <typeparam name="TTo">The type of the object that should be returned.</typeparam>
        /// <typeparam name="TRet">The type of the object that should be returned.</typeparam>
        /// <typeparam name="TFrom">The type of the base object that should be used as a template.</typeparam>
        public static TTo As<TFrom,TTo,TRet>(this TFrom t, Expression<Func<TTo,TRet>> expr, TRet val)
        {
            var memberAccess = new ExpressionWithMemberAccess();
            memberAccess.Lambda(expr);
            return CreateInstanceFromValues.Create<TTo>(t,new[] { GetNameAndValue.Get(t, memberAccess.Members, val) });
        }
/*
        public static TRet As<TRet>(this Object t, Expression<Func<TRet, bool>> expr)
        {
            var eqeq = new ExpressionWithEqualEqualOrCall<TRet>(t);
            eqeq.Lambda(expr);
            var propertyNameAndValues = eqeq.Parsed.ToArray();
            return CreateInstanceFromValues.Create<TRet>(t, propertyNameAndValues);
        }
*/
    }
}
