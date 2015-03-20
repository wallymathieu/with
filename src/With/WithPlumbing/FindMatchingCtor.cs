using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace With.WithPlumbing
{
    using Rubyfy;
    class FindMatchingCtor<T>
    {
        public ConstructorInfo Get()
        {
            var ctors = typeof(T).GetConstructors().ToA();
            if (ctors.Length == 1)
            {
                return ctors.First();
            }
            return ctors.Max((ctor1, ctor2) => ParameterLength(ctor1).CompareTo(ParameterLength(ctor2)));
        }

        private int ParameterLength(ConstructorInfo ctor)
        {
            return ctor.GetParameters().Length;
        }
    }
}
