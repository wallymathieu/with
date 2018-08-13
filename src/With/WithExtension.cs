using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using With.Internals;
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
    }

}
