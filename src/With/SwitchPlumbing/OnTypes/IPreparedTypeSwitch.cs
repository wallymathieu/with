using System;

namespace With.SwitchPlumbing
{
	
	public interface IPreparedTypeSwitch{
		Object Instance {
			get;
			set;
		}
		bool TryGetValue(out object value);
	}
}