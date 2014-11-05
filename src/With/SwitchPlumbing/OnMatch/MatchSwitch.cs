using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public class MatchSwitch<Ingoing,Outgoing>:IMatchSwitch<Ingoing, Outgoing>{
		public override bool TryMatch (out Outgoing value)
		{
			value = default(Outgoing);
			return false;
		}

		public override Ingoing Instance {
			get;
			set;
		}

	}
}