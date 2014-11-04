using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public class MatchSwitch<Ingoing>:IMatchSwitch<Ingoing>{
		public bool TryMatch ()
		{
			return false;
		}

		public Ingoing Instance {
			get;
			set;
		}
	}
}