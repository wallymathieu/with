using System;
using System.Reflection;

namespace With.Reflection
{
    internal class FieldOrPropertyOrGetMethod
    {
        public FieldInfo Field { get; private set; }

        public string Name
        {
            get
            {
                if (MethodInfo != null)
                    return RemoveGetFromBeginningOfString(MethodInfo.Name);
                if (Property != null)
                    return Property.Name;
                if (Field != null)
                    return Field.Name;
                throw new Exception("Missing!");
            }
        }
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
        }
        public FieldOrPropertyOrGetMethod(FieldInfo fieldInfo)
        {
            Field = fieldInfo;
        }
        public FieldOrPropertyOrGetMethod(PropertyInfo property)
        {
            Property = property;
        }

        public MethodInfo MethodInfo { get; private set; }
        public PropertyInfo Property { get; private set; }
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

        public Type ReturnType
        {
            get
            {
                if (MethodInfo != null)
                    return MethodInfo.ReturnType;
                if (Property != null)
                    return Property.PropertyType;
                if (Field != null)
                    return Field.FieldType;
                throw new Exception("Missing!");
            }
        }
    }
}
