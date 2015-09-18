using System;
using System.Collections.Generic;
using System.Linq;

namespace With.Destructure
{
    using Linq;
    using Reflection;
    internal class Fields
    {
        FieldOrPropertyOrGetMethod[] fields;

        public Fields(Type type, TypeOfFIelds typeOfFIelds)
        {
            fields = type.GetFieldsOrPropertiesOrGetMethods(typeOfFIelds).ToArray();
        }

        public IEnumerable<string> GetNames()
        {
            return fields.Select(f => f.Name).OrderBy(v=>v);
        }

        internal bool IsTupleMatch(Type[] matches)
        {
            return fields.Select(f => f.ReturnType).SequenceEqual(matches);
        }

        internal object[] GetValues(Object instance, Type[] matches)
        {
            return fields.Select(f => f.GetValue(instance)).ToArray();
        }
    }
}
