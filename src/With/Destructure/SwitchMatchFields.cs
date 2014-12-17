using System;
using With.Destructure;
using With.SwitchPlumbing;
using System.Linq;
using With.Rubyfy;
using System.Collections.Generic;
namespace With.Destructure
{
    internal class SwitchMatchFields<In, Out> : ISwitch<In, Out>
    {
        private readonly ISwitch<In, Out> inner;
        private readonly IEnumerable<Tuple<Type[], Delegate>> matches;
        private readonly TypeOfFIelds typeOfFields;

        public SwitchMatchFields(ISwitch<In, Out> inner, Delegate[] funcs, TypeOfFIelds typeOfFields)
        {
            this.typeOfFields = typeOfFields;
            this.inner = inner;
            this.matches = funcs.Map(f => Tuple.Create(f.Method.GetParameters().Select(p => p.ParameterType).ToArray(), f)).ToA();
        }

        public override bool TryMatch(out Out value)
        {
            if (inner.TryMatch(out value))
            {
                return true;
            }
            var fields = new Fields(Instance.GetType(), typeOfFields);
            foreach (var kv in matches)
            {
                if (fields.IsTupleMatch(kv.Item1))
                {
                    value = (Out)kv.Item2.DynamicInvoke(fields.GetValues(Instance, kv.Item1));
                    return true;
                }
            }
            value = default(Out);
            return false;
        }

        public override In Instance
        {
            get { return inner.Instance; }
            set { inner.Instance = value; }
        }
    }
}
