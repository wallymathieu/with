using System;
using System.Reflection;

namespace With.Reflection
{
    internal class FieldOrPropertyOrGetMethod
    {
        public readonly FieldInfo Field;
        public readonly MethodInfo MethodInfo;
        public readonly PropertyInfo Property;
        public readonly string Name;
        public readonly Type ReturnType;


        private static string RemoveGetFromBeginningOfString(string arg)
        {
            if (arg.StartsWith("get_", StringComparison.InvariantCultureIgnoreCase))
                return arg.Substring(4);
            if (arg.StartsWith("get", StringComparison.InvariantCultureIgnoreCase))
                return arg.Substring(3);
            return arg;
        }

        public FieldOrPropertyOrGetMethod(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            Name = RemoveGetFromBeginningOfString(MethodInfo.Name);
            ReturnType = MethodInfo.ReturnType;
        }

        public FieldOrPropertyOrGetMethod(FieldInfo fieldInfo)
        {
            Field = fieldInfo;
            Name = Field.Name;
            ReturnType = Field.FieldType;
        }

        public FieldOrPropertyOrGetMethod(PropertyInfo property)
        {
            Property = property;
            Name = Property.Name;
            ReturnType = Property.PropertyType;
        }

        public object GetValue(object instance)
        {
            if (MethodInfo != null)
                return MethodInfo.Invoke(instance, null);
            if (Property != null)
                return Property.GetValue(instance, null);
            if (Field != null)
                return Field.GetValue(instance);
            throw new Exception("Missing!");
        }
    }
}
