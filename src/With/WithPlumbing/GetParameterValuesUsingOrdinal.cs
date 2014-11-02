using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace With.WithPlumbing
{
	internal class GetParameterValuesUsingOrdinal
	{
		public object[] GetValues(Object t, Type tret, Object[] parameters)
		{
			var props = t.GetType().GetProperties();
			var ctors = tret.GetConstructors().ToArray();
			var ctor = ctors.Single();
			var ctorParams = ctor.GetParameters();
			var values = new object[ctorParams.Length];
			for (int i = 0; i < values.Length - parameters.Length; i++)
			{
				var param = ctorParams[i];
				object val;
				val = GetValue (t, props, param);
				values [i] = val;
			}

			parameters.CopyTo(values, values.Length - parameters.Length);
			return values;
		}
			
		static object GetValue (object t, IEnumerable<PropertyInfo> props, ParameterInfo param)
		{
			object val;
			var p = props.SingleOrDefault (prop => prop.Name.Equals (param.Name, StringComparison.InvariantCultureIgnoreCase));
			if (p != null) {
				val = p.GetValue (t, null);
			}
			else {
				throw new MissingValueException (param.Name);
			}
			return val;
		}
	}
	
}
