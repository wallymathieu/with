using System;

namespace With.SwitchPlumbing
{
	public class PreparedTypeSwitch<T, TRet> : IPreparedTypeSwitch
    {
		private readonly IPreparedTypeSwitch _preparedSwitch;
		private readonly Func<T,TRet> _func;
		public PreparedTypeSwitch(IPreparedTypeSwitch preparedSwitch, Func<T, TRet> @case)
        {
            _preparedSwitch = preparedSwitch;
			_func = @case;
        }

        public object ValueOf(object instance)
        {
			Instance = instance;
            return Value();
        }

		public Object Instance {
			get{ return _preparedSwitch.Instance; }
			set{ _preparedSwitch.Instance = value; }
		}

		public bool TryGetValue(out object value)
		{
			if (_preparedSwitch.TryGetValue(out value))
			{
				return true;
			}
			var instance = Instance;
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