using System;

namespace With.SwitchPlumbing
{
    public class PreparedTypeSwitch<T, TRet> : TypeSwitchOn<T, TRet>, IPreparedSwitch
    {
        private readonly IPreparedSwitch _preparedSwitch;

        public PreparedTypeSwitch(IPreparedSwitch preparedSwitch, Func<T, TRet> @case)
            : base((TypeSwitchOnBase)preparedSwitch, @case)
        {
            _preparedSwitch = preparedSwitch;
        }

        public object ValueOf(object instance)
        {
            SetInstance(instance);
            return Value();
        }

        public new PreparedTypeSwitch<T1, TRet1> Case<T1, TRet1>(Func<T1, TRet1> func)
        {
            return new PreparedTypeSwitch<T1, TRet1>(this, func);
        }

        public void SetInstance(object instance)
        {
            _preparedSwitch.SetInstance(instance);
        }
    }
}