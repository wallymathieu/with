using System;

namespace With.SwitchPlumbing
{
    public class TypeSwitchOn<T, TRet> : TypeSwitchOnBase
    {
        private readonly TypeSwitchOnBase _switchOn;
        private readonly Func<T, TRet> _func;

        public TypeSwitchOn(TypeSwitchOnBase switchOn, Func<T, TRet> func)
        {
            _switchOn = switchOn;
            _func = func;
        }

        public object Value()
        {
            object value;
            return TryGetValue(out value) ? value : null;
        }

		public static implicit operator TRet(TypeSwitchOn<T, TRet> d)
		{
			return (TRet)d.Value ();
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
}