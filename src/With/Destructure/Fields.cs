using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace With.Destructure
{
    using Rubyfy;
    using Reflection;
    internal class Fields
    {
        FieldOrPropertyOrGetMethod[] fields;

        public Fields(Type type, TypeOfFIelds typeOfFIelds)
        {
            fields = type.GetFieldsOrPropertiesOrGetMethods(typeOfFIelds).ToA();
        }

        public IEnumerable<string> GetNames()
        {
            return fields.Map(f => f.Name).Sort();
        }

        internal bool IsTupleMatch(Type[] matches)
        {
            return fields.Map(f => f.ReturnType).SequenceEqual(matches);
        }

        internal object[] GetValues(Object instance, Type[] matches)
        {
            return fields.Map(f => f.GetValue(instance)).ToA();
        }
    }
}
