using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using With.Rubyfy;

namespace With.Destructure
{
    internal class Fields
    {
        GetMethodInfoOrFieldOrProperty[] fields;

        public Fields(Type type, TypeOfFIelds typeOfFIelds)
        {
            var list = new List<GetMethodInfoOrFieldOrProperty>();
            if (typeOfFIelds.HasFlag(TypeOfFIelds.Methods))
            {
                list.AddRange(GetPublicGetMethods(type)
                 .Map(m => new GetMethodInfoOrFieldOrProperty(m)));
            }

            if (typeOfFIelds.HasFlag(TypeOfFIelds.Properties))
            {
                list.AddRange(GetPublicProperties(type)
                 .Map(m => new GetMethodInfoOrFieldOrProperty(m)));
            }

            if (typeOfFIelds.HasFlag(TypeOfFIelds.Fields))
            {
                list.AddRange(GetPublicFields(type)
                 .Map(m => new GetMethodInfoOrFieldOrProperty(m)));
            }
            fields = list.ToA();
        }

        private IEnumerable<PropertyInfo> GetPublicProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private static Regex _getNotUnderscore = new Regex("^get[^_]", RegexOptions.IgnoreCase);
        private static IEnumerable<MethodInfo> GetPublicGetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.DeclaringType != typeof(Object) && m.DeclaringType.IsValueType
                    && m.Name.Match(_getNotUnderscore).Success);
        }

        private static IEnumerable<FieldInfo> GetPublicFields(Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public);
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
