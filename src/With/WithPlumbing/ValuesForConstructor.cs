using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using NameAndValue = System.Collections.Generic.KeyValuePair<string, object>;

namespace With.WithPlumbing
{
    public class ValuesForConstructor<TDestination>
    {
        private readonly Object parent;
        private readonly IList<NameAndValue> values;

        public ValuesForConstructor(Object parent)
        {
            this.parent = parent;
            this.values = new List<NameAndValue> ();
        }

        public ValuesForConstructor<TDestination> Eql<TValue>(Expression<Func<TDestination, TValue>> expr, TValue val)
        {
            var memberAccess = new ExpressionWithMemberAccess();
            memberAccess.Lambda<TDestination, TValue>(expr);
            values.Add(GetNameAndValue.Get(parent, memberAccess.Members, val));
            return this;
        }

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
