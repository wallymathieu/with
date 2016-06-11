using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using NameAndValue = System.Collections.Generic.KeyValuePair<string, object>;

namespace With.WithPlumbing
{
    /// <summary>
    /// 
    /// </summary>
    public class ValuesForConstructor<TDestination>
    {
        private readonly Object parent;
        private readonly IList<NameAndValue> values;

        public ValuesForConstructor(Object parent)
        {
            this.parent = parent;
            this.values = new List<NameAndValue> ();
        }

        /// <summary>
        /// Specify a value for a property, in a future new object.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="property">Expression to specify the property</param>
        /// <param name="value">The value that the property of the new object is supposed to have</param>
        /// <returns></returns>
        public ValuesForConstructor<TDestination> Eql<TValue>(Expression<Func<TDestination, TValue>> property, TValue value)
        {
            var memberAccess = new ExpressionWithMemberAccess();
            memberAccess.Lambda<TDestination, TValue>(property);
            values.Add(GetNameAndValue.Get(parent, memberAccess.Members, value));
            return this;
        }

        /// <summary>
        /// Returns instance with values specified
        /// </summary>
        public TDestination To()
        {
            return CreateInstanceFromValues.Create<TDestination> (parent, values);
        }

        public static implicit operator TDestination(ValuesForConstructor<TDestination> d)
        {
            return d.To();
        }
    }
}
