using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With
{
    public class Switch
    {
        public class SwitchOnWithCase<T, TRet> : SwitchOn
        {
            private readonly SwitchOn _switchOn;
            private readonly Func<T, TRet> _func;

            public SwitchOnWithCase(SwitchOn switchOn, Func<T, TRet> func)
            {
                _switchOn = switchOn;
                _func = func;
            }

            public object Value()
            {
                object value;
                if (TryGetValue(out value))
                {
                    return value;
                }
                return null;
            }

            protected internal override bool TryGetValue(out object value)
            {
                if (_switchOn.TryGetValue(out value))
                {
                    return true;
                }
                var instance = _switchOn.GetInstance();
                if (instance is T)
                {
                    value = _func((T)instance);
                    return true;
                }
                value = null;
                return false;
            }

            protected internal override object GetInstance()
            {
                return _switchOn.GetInstance();
            }
        }

        public abstract class SwitchOn
        {
            public SwitchOnWithCase<T, TRet> Case<T, TRet>(Func<T, TRet> func)
            {
                var @case = new SwitchOnWithCase<T, TRet>(this, func);
                return @case;
            }

            protected internal abstract bool TryGetValue(out object value);

            protected internal abstract object GetInstance();
        }

        public class SwitchOnInstance : SwitchOn
        {
            private readonly object _instance;

            public SwitchOnInstance(object instance)
            {
                _instance = instance;
            }

            protected internal override bool TryGetValue(out object value)
            {
                value = null;
                return false;
            }

            protected internal override object GetInstance()
            {
                return _instance;
            }
        }

        public static SwitchOnInstance On(object instance)
        {
            return new SwitchOnInstance(instance);
        }
    }
}
