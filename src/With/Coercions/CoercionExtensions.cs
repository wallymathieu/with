using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.Internals;
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
    }
}
