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

	public class MatchSwitch<Ingoing,Outgoing>:MatchSwitch<Ingoing>{
		public MatchSwitch<Ingoing,Outgoing> Case (Ingoing expected, Func<Outgoing> result)
		{
			throw new NotImplementedException ();
		}

		public MatchSwitch<Ingoing,Outgoing> Case (IEnumerable<Ingoing> expected, Func<Ingoing,Outgoing> result)
		{
			throw new NotImplementedException ();
		}

		public MatchSwitch<Ingoing,Outgoing> Else (Func<Ingoing,Outgoing> result)
		{
			throw new NotImplementedException ();
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