using System;

namespace With.SwitchPlumbing
{
    public class PreparedTypeSwitch : TypeSwitchOn, IPreparedSwitch
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

        public new PreparedTypeSwitchCase<T, TRet> Case<T, TRet>(Func<T, TRet> func)
        {
            return new PreparedTypeSwitchCase<T, TRet>(this, func);
        }
    }
}