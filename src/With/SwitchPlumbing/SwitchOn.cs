using System;

namespace With.SwitchPlumbing
{
    public abstract class SwitchOn
    {
        public virtual SwitchOnWithCase<T, TRet> Case<T, TRet>(Func<T, TRet> func)
        {
            return new SwitchOnWithCase<T, TRet>(this, func);
        }

        protected internal abstract bool TryGetValue(out object value);

        protected internal abstract object GetInstance();
    }
}