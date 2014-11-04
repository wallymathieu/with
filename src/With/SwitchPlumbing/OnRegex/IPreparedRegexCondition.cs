using System;
using System.Text.RegularExpressions;

namespace With.SwitchPlumbing
{
	public interface IPreparedRegexCondition{
		string Instance {
			get;
			set;
		}
		bool TryGetValue(out object value);
	}    

}
