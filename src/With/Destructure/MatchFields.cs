using System;
using With.Destructure;
using With.SwitchPlumbing;
using System.Linq;
namespace With
{
    public class MatchFields<In, Out> : ISwitch<In, Out>
    {
        private readonly ISwitch<In, Out> inner;
        private readonly Delegate func;
        private readonly Type[] matches;

        public MatchFields(ISwitch<In, Out> inner, Delegate func)
        {
            this.inner = inner;
            this.func = func;
            var args = func.Method.GetGenericArguments();
            this.matches = func.Method.GetParameters().Select(p=>p.ParameterType).ToArray();
        }

        public override bool TryMatch(out Out value)
        {
            if (inner.TryMatch(out value))
            {
                return true;
            }
            var fields = new Fields(Instance.GetType());
            if (fields.IsTupleMatch(matches))
            {
                value = (Out)func.DynamicInvoke(fields.GetValues(Instance, matches));
                return true;
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
