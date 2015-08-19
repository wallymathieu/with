using System;
using System.Linq;
using System.Collections.Generic;
namespace With.Destructure
{
    using Rubyfy;
    using Reflection;
    using SwitchPlumbing;

    internal class SwitchMatchFields<In, Out> : IMatcher<In, Out>
    {
        private readonly IEnumerable<Tuple<Type[], Delegate>> matches;
        private readonly TypeOfFIelds typeOfFields;

        public SwitchMatchFields(Delegate[] funcs, TypeOfFIelds typeOfFields)
        {
            this.typeOfFields = typeOfFields;
            this.matches = funcs.Map(f => Tuple.Create(f.Method.GetParameters().Select(p => p.ParameterType).ToArray(), f)).ToA();
        }

        public bool TryMatch(In instance, out Out value)
        {
            var fields = new Fields(instance.GetType(), typeOfFields);
            foreach (var kv in matches)
            {
                if (fields.IsTupleMatch(kv.Item1))
                {
                    value = (Out)kv.Item2.DynamicInvoke(fields.GetValues(instance, kv.Item1));
                    return true;
                }
            }
            value = default(Out);
            return false;
        }
    }
}
