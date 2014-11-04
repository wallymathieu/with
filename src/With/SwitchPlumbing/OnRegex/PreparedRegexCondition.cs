using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
	public class PreparedRegexCondition :IPreparedRegexCondition
    {
		public string Instance {
			get;
			set;
		}

		public bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }


    }

}
