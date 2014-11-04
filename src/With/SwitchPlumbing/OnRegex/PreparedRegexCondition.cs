using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
    
    public class PreparedRegexCondition 
    {
        private string _string;

        public virtual void SetString(string @string)
        {
            _string = @string;
        }

		protected internal virtual bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }

		protected internal virtual string GetString()
        {
            return _string;
        }

		public virtual PreparedRegexCondition<TRet1> Case<TRet1>(string regex, Func<Match, TRet1> func)
        {
			return new PreparedRegexCondition<TRet1>(this, regex, func);
        }
    }

}