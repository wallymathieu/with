using System;
using System.Collections.Generic;

namespace With.SwitchPlumbing
{
	public class MatchSwitch<Ingoing,Outgoing>:IMatchSwitch<Ingoing,Outgoing>{
		public Ingoing Instance {
			get;
			set;
		}

		public bool TryMatch ()
		{
			return false;
		}
		public bool TryMatch (out Outgoing value)
		{
			value = default(Outgoing);
			return false;
		}
		public Outgoing Value()
		{
			throw new NotImplementedException ();
		}

		public static implicit operator Outgoing(MatchSwitch<Ingoing,Outgoing> d)
		{
			return d.Value ();
		}

	}
}