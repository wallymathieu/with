using System.Reflection;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
namespace With.Reflection
{
    using Linq;

    static class ConstructorExtensions 
    {
        private static ConditionalWeakTable<Type, ConstructorInfo> constructorWithMostParameters = new ConditionalWeakTable<Type, ConstructorInfo>();

        public static ConstructorInfo GetConstructorWithMostParameters(this Type t)
        {
            return constructorWithMostParameters.WeakMemoize(t, type =>
             {
                 var ctors = type.GetTypeInfo().GetConstructors().ToArray();
                 if (ctors.Length == 1)
                 {
                     return ctors.First();
                 }
                 return ctors.Max((ctor1, ctor2) => ParameterLength(ctor1).CompareTo(ParameterLength(ctor2)));
             });
        }

        private static int ParameterLength(ConstructorInfo ctor)
        {
            return ctor.GetParameters().Length;
        }
    }
}
