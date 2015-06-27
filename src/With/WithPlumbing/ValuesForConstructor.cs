using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NameAndValue = System.Collections.Generic.KeyValuePair<string,object>;

namespace With.WithPlumbing
{
    using Reflection;
    public class ValuesForConstructor<TDestination>
    {
        private readonly Object parent;
        private readonly CreateInstanceFromValues<TDestination> createInstance;
        private readonly IList<NameAndValue> values;

        public ValuesForConstructor(Object parent)
        {
            this.parent = parent;
            this.values = new List<NameAndValue> ();
            this.createInstance = new CreateInstanceFromValues<TDestination> (parent.GetType());
        }

        public ValuesForConstructor<TDestination> Eql<TValue>(Expression<Func<TDestination, TValue>> expr, TValue val)
        {
            var memberAccess = new ExpressionWithMemberAccess<TDestination, TValue>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;
            values.Add(new NameAndValue(propertyName, val));
            return this;
        }

        public TDestination To()
        {
            return createInstance.Create (parent, values);
        }

        public static implicit operator TDestination(ValuesForConstructor<TDestination> d)
        {
            return d.To();
        }
    }
}
