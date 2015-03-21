using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace With.WithPlumbing
{
    using Reflection;
    public class ValuesForConstructor<T>
    {
        private readonly Object parent;
        private readonly FieldOrProperty[] props;
        private readonly ConstructorInfo ctor;
        private readonly IList<NameAndValue> values;

        public ValuesForConstructor(Object parent)
        {
            this.parent = parent;
            this.props = typeof(T).GetFieldsOrProperties();
            ctor = typeof(T).GetConstructorWithMostParameters();
            values = new List<NameAndValue>();
        }
        public ValuesForConstructor<T> Eql<TValue>(Expression<Func<T, TValue>> expr, TValue val)
        {
            var memberAccess = new ExpressionWithMemberAccess<T, TValue>();
            memberAccess.Lambda(expr);
            var propertyName = memberAccess.MemberName;
            values.Add(new NameAndValue(propertyName, val));
            return this;
        }

        public T To()
        {
            return (T)ctor.Invoke(GetConstructorParamterValues.GetValues(parent, values, props, ctor));
        }

        public static implicit operator T(ValuesForConstructor<T> d)
        {
            return d.To();
        }
    }
}
