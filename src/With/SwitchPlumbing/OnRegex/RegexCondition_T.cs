using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
	public class RegexCondition<TRet> : PreparedRegexCondition
    {
		private readonly PreparedRegexCondition _base;
        private readonly Regex _regex;
        private readonly Func<Match, TRet> _func;

		public RegexCondition(PreparedRegexCondition @base, string regex, Func<Match, TRet> func)
        {
            _base = @base;
            _regex = new Regex(regex);
            _func = func;
        }

		protected internal override bool TryGetValue(out object value)
        {
            if (_base.TryGetValue(out value))
            {
                return true;
            }
            var instance = _base.GetString();
            var m = _regex.Match(instance);
            if (m.Success)
            {
                value = _func(m);
                return true;
            }
            value = null;
            return false;
        }

		protected internal override string GetString()
        {
            return _base.GetString();
        }

        public virtual object Value()
        {
            object value;
            return TryGetValue(out value) ? value : null;
        }
		public static implicit operator TRet(RegexCondition<TRet> d)
		{
			return (TRet) d.Value ();
		}
    }
}