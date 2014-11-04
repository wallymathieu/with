using System;
using System.Collections.Generic;

namespace With.SwitchPlumbing
{
	public class MatchSwitch<Ingoing>{
		public MatchSwitch<Ingoing> Case (Ingoing expected, Action result)
		{
			throw new NotImplementedException ();
		}

		public MatchSwitch<Ingoing> Case (IEnumerable<Ingoing> expected, Action<Ingoing> result)
		{
			throw new NotImplementedException ();
		}

		public void Else (Action<Ingoing> result)
		{
			throw new NotImplementedException ();
		}
	}
	
}