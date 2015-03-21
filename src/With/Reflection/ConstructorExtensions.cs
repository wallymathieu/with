using System.Reflection;

namespace With.Reflection
{
    using Rubyfy;
    using System;
    using System.Runtime.CompilerServices;

    static class ConstructorExtensions 
    {
        private static ConditionalWeakTable<Type, ConstructorInfo> constructorWithMostParameters = new ConditionalWeakTable<Type, ConstructorInfo>();

        public static ConstructorInfo GetConstructorWithMostParameters(this Type t)
        {
            return constructorWithMostParameters.WeakMemoize(t, type =>
             {
                 var ctors = type.GetConstructors().ToA();
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
