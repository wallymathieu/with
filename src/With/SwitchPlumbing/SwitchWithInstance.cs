using System;

namespace With.SwitchPlumbing
{
    public class SwitchWithInstance<In, Out> : MatchCollector<SwitchWithInstance<In, Out>, In, Out>
    {
        private readonly Switch<In,Out> _switch;
        private readonly In _instance;
        public SwitchWithInstance(In instance, Switch<In,Out> @switch)
        {
            _instance = instance;
            _switch = @switch;
        }

        public bool TryMatch(out Out value)
        {
            return _switch.TryMatch(_instance, out value);
        }

        public Out Value()
        {
            return _switch.ValueOf(_instance);
        }

        public override SwitchWithInstance<In, Out> Add(IMatcher<In, Out> m)
        {
            return new SwitchWithInstance<In, Out>(_instance, _switch.Add(m));
        }

        public static implicit operator Out(SwitchWithInstance<In, Out> d)
        {
            Out value;
            if (d.TryMatch(out value))
                return value;
            throw new NoMatchFoundException();
        }

    }
}

