using System;

namespace With.SwitchPlumbing
{
	public class PreparedTypeSwitch:IPreparedTypeSwitch
    {
		public Object Instance {
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