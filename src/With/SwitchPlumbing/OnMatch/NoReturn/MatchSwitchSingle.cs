using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{

	public class MatchSwitchSingle<Ingoing>:IMatchSwitch<Ingoing>{
		private readonly Ingoing expected;
		private readonly Action result;
		private readonly IMatchSwitch<Ingoing> inner;
		public Ingoing Instance {
			get{ return inner.Instance;}
			set{ inner.Instance = value;}
		}
		public bool TryMatch ()
		{
			if (inner.TryMatch ()) {
				return true;
			}
			if (expected.Equals(Instance)) {
				result ();
				return true;
			}
			return false;
		}
		public MatchSwitchSingle (IMatchSwitch<Ingoing> inner, Ingoing expected, Action result)
		{
			this.inner = inner;
			this.expected = expected;
			this.result = result;
		}

	}
}