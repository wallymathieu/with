using System;

namespace With.SwitchPlumbing
{
    public abstract class TypeSwitchOnBase
    {
		public virtual TypeSwitchOn<T, TRet> Case<T, TRet>(Func<T, TRet> func)
        {
			return new TypeSwitchOn<T, TRet>(this, func);
        }

        protected internal abstract bool TryGetValue(out object value);

        protected internal abstract object GetInstance();
    }
}