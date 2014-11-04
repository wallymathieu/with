using System;

namespace With.SwitchPlumbing
{
	public class PreparedTypeSwitch<T, TRet> : PreparedTypeSwitch
    {
		private readonly PreparedTypeSwitch _preparedSwitch;
		private readonly Func<T,TRet> _func;
		public PreparedTypeSwitch(PreparedTypeSwitch preparedSwitch, Func<T, TRet> @case)
        {
            _preparedSwitch = preparedSwitch;
			_func = @case;
        }

        public object ValueOf(object instance)
        {
            SetInstance(instance);
            return Value();
        }

        public override void SetInstance(object instance)
        {
            _preparedSwitch.SetInstance(instance);
        }

		protected internal override object GetInstance ()
		{
			return _preparedSwitch.GetInstance ();
		}

		protected internal override bool TryGetValue(out object value)
		{
			if (_preparedSwitch.TryGetValue(out value))
			{
				return true;
			}
			var instance = _preparedSwitch.GetInstance();
			if (instance is T)
			{
				value = _func((T)instance);
				return true;
			}
			value = null;
			return false;
		}

		public object Value()
		{
			object value;
			return TryGetValue(out value) ? value : null;
		}

		public static implicit operator TRet(PreparedTypeSwitch<T, TRet> d)
		{
			return (TRet) d.Value ();
		}
    }
}