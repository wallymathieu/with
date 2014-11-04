using System;

namespace With.SwitchPlumbing
{
    public class PreparedTypeSwitch
    {
        private object _instance;

        protected internal virtual bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }

		protected internal virtual object GetInstance()
        {
            return _instance;
        }

        public virtual void SetInstance(object instance)
        {
            _instance = instance;
        }

		public PreparedTypeSwitch<T, TRet> Case<T, TRet>(Func<T, TRet> func)
		{
			return new PreparedTypeSwitch<T, TRet>(this, func);
		}

    }
}