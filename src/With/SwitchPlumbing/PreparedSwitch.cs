using System;

namespace With.SwitchPlumbing
{
    public class PreparedSwitch : SwitchOn, IPreparedSwitch
    {
        private object _instance;

        protected internal override bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }

        protected internal override object GetInstance()
        {
            return _instance;
        }

        public void SetInstance(object instance)
        {
            _instance = instance;
        }

        public new PreparedSwitchCase<T, TRet> Case<T, TRet>(Func<T, TRet> func)
        {
            return new PreparedSwitchCase<T, TRet>(this, func);
        }
    }
}