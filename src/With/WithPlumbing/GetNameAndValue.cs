using System;
using System.Reflection;
using NameAndValue = System.Collections.Generic.KeyValuePair<string, object>;
using System.Linq;
using With.Reflection;
using System.Collections.Generic;

namespace With.WithPlumbing
{
    class GetNameAndValue
    {
        internal static NameAndValue Get(Object t, IEnumerable<MemberInfo> members, object val)
        {
            var ms = members.ToArray();
            return new NameAndValue(ms[0].Name, ms.Length == 1
                ? val
                : ValueOf(t, ms, val));
        }
        private static object ValueOf(object t, MemberInfo[] memberNames, object val)
        {
            var values = GetMemberValues(t, memberNames, val);
            UpdateValues(memberNames, values);
            return values[0];
        }

        private static void UpdateValues(MemberInfo[] members, object[] values)
        {
            // walk backwards and create new instances with updated value
            // i.e.
            // if we have :
            // memberNames = ["Sale", "Customer", "Name"]
            // customer = new Customer("Kund")
            // values = [new Sale(customer), customer, "Allan"]
            // 
            // First create a new 'customer' with name "Allan"
            // that is, customer is the value at values[members.Length-2]
            // Then we create a new 'sale' with customer set to our newly created
            // customer
            for (int i = members.Length - 2; i >= 0; i--)
            {
                var currentValue = values[i];
                var nextValue = values[i + 1];
                var type = currentValue.GetType();
                var nextName = members[i + 1].Name;
                values[i] = CreateInstanceFromValues.Create(type, type,
                    currentValue,
                    new[] { new NameAndValue(nextName, nextValue) });
            }
        }

        private static object[] GetMemberValues(object t, MemberInfo[] members, object val)
        {
            var values = new object[members.Length];
            var c = t;
            for (int i = 0; i < members.Length - 1; i++)
            {
                var ctype = c.GetType();
                var field = ctype.GetFieldsOrProperties().Single(f => f.Name == members[i].Name);
                values[i] = field.GetValue(c);
            }
            // since we have the final value 'val', we don't need to do field.GetValue for it
            values[members.Length - 1] = val;
            return values;
        }
    }
}
