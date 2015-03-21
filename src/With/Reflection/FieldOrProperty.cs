using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace With.Reflection
{
    internal class FieldOrProperty
    {
        public readonly PropertyInfo Property;
        public readonly FieldInfo FieldInfo;
        public readonly string Name;

        public FieldOrProperty(PropertyInfo Property)
        {
            this.Property = Property;
            Name = Property.Name;
        }

        public FieldOrProperty(FieldInfo fieldInfo)
        {
            this.FieldInfo = fieldInfo;
            Name = fieldInfo.Name;
        }

        internal object GetValue(object t)
        {
            if (Property != null)
            {
                return Property.GetValue(t, null);
            }
            else
            {
                return FieldInfo.GetValue(t);
            }
        }
    }
}
