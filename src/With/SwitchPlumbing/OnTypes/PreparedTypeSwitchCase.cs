using System;

namespace With.SwitchPlumbing
{
    public class PreparedTypeSwitchCase<T, TRet> : SwitchOnWithCase<T, TRet>, IPreparedSwitch
    {
        private readonly IPreparedSwitch _preparedSwitch;

        public PreparedTypeSwitchCase(IPreparedSwitch preparedSwitch, Func<T, TRet> @case)
            : base((TypeSwitchOn)preparedSwitch, @case)
        {
            _preparedSwitch = preparedSwitch;
        }

        public object ValueOf(object instance)
        {
            SetInstance(instance);
            return Value();
        }

        public new PreparedTypeSwitchCase<T1, TRet1> Case<T1, TRet1>(Func<T1, TRet1> func)
        {
            return new PreparedTypeSwitchCase<T1, TRet1>(this, func);
        }

        public void SetInstance(object instance)
        {
            _preparedSwitch.SetInstance(instance);
        }
    }
}