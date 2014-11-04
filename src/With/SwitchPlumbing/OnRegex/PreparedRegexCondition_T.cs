using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
    
	public class PreparedRegexCondition<TRet> : IPreparedRegexCondition
    {
		public PreparedRegexCondition(IPreparedRegexCondition preparedSwitch, string regex, Func<Match, TRet> @case)
        {
			_regex = new Regex(regex);
			_func = @case;
			_base = preparedSwitch;
        }

		public string Instance {
			get{ return _base.Instance;}
			set{ _base.Instance = value;}
		}


        public object ValueOf(string instance)
        {
			Instance = instance;
            return Value();
        }


		private readonly IPreparedRegexCondition _base;
		private readonly Regex _regex;
		private readonly Func<Match, TRet> _func;

		public bool TryGetValue(out object value)
		{
			if (_base.TryGetValue(out value))
			{
				return true;
			}
			var instance = Instance;
			var m = _regex.Match(instance);
			if (m.Success)
			{
				value = _func(m);
				return true;
			}
			value = null;
			return false;
		}

		public virtual object Value()
		{
			object value;
			return TryGetValue(out value) ? value : null;
		}

		public static implicit operator TRet(PreparedRegexCondition<TRet> d)
		{
			return (TRet) d.Value ();
		}

    }

}
