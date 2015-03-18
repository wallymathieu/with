using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace With.Reflection
{
	internal class FieldOrProperty
	{
		public PropertyInfo Property { get; private set; }

		public string Name
		{
			get; private set;
		}

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
		public FieldInfo FieldInfo { get; private set; }

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
