using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public class MatchSwitch<In,Out>:IMatchSwitch<In, Out>{
		public override bool TryMatch (out Out value)
		{
			value = default(Out);
			return false;
		}

		public override In Instance {
			get;
			set;
		}

	}
}