using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.WithPlumbing;

namespace With
{
    /// <summary>
    /// With extensions. Used to create 
    /// </summary>
    public static class WithExtensions
    {
        /// <summary>
        /// Starts constructing another object based on a base object.
        /// </summary>
        /// <returns>A builder for getting a new object based on the existing object.</returns>
        /// <param name="t">The object to use as a template.</param>
        /// <typeparam name="T">The type of the object that should be returned.</typeparam>
        public static ValuesForConstructor<T> With<T>(this T t)
        {
            return new ValuesForConstructor<T>(t);
        }
        /// <summary>
        /// Constructs another object based on a base object.
        /// </summary>
        /// <returns>A new object based on the existing object.</returns>
        /// <param name="t">The object to use as a template.</param>
        /// <param name="expr">What property or field to be different</param>
        /// <param name="val">The value for the property expected to be changed</param>
        /// <typeparam name="T">The type of the object that should be returned.</typeparam>
        public static T With<T, TVal>(this T t, Expression<Func<T, TVal>> expr, TVal val)
        {
            var memberAccess = new ExpressionWithMemberAccess();
            memberAccess.Lambda<T, TVal>(expr);

            return CreateInstanceFromValues.Create<T,T>(t,new[] { GetNameAndValue.Get(t, memberAccess.Members, val) });
        }
        /// <summary>
        /// Constructs another object based on a base object.
        /// </summary>
        /// <returns>A new object based on the existing object.</returns>
        /// <param name="t">The object to use as a template.</param>
        /// <param name="parameters">The constructor parameters to override.</param>
        /// <typeparam name="T">The type of the object that should be returned.</typeparam>
        public static T With<T>(this T t, IDictionary<string, object> parameters)
        {
            return CreateInstanceFromValues.Create<T,T>(t,parameters);
        }
/*
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
*/
    }
}
