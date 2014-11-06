using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{

	public class MatchSwitchSingle<Ingoing,Outgoing>:IMatchSwitch<Ingoing,Outgoing>{
		private readonly Ingoing expected;
		private readonly Func<Ingoing,Outgoing> result;
		private readonly IMatchSwitch<Ingoing,Outgoing> inner;
		public override Ingoing Instance {
			get{ return inner.Instance;}
			set{ inner.Instance = value;}
		}
		public override bool TryMatch (out Outgoing value)
		{
			if (inner.TryMatch (out value)) {
				return true;
			}
			if (expected.Equals(Instance)) {
				value = result (Instance);
				return true;
			}
			return false;
		}
		public MatchSwitchSingle (IMatchSwitch<Ingoing,Outgoing> inner, Ingoing expected, Func<Ingoing,Outgoing> result)
		{
			this.inner = inner;
			this.expected = expected;
			this.result = result;
		}

	}
}