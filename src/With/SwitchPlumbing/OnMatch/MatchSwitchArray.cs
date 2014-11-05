using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{

	public class MatchSwitchArray<Ingoing,Outgoing>:IMatchSwitch<Ingoing,Outgoing>{
		private readonly IList<Ingoing> expected;
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
			if (expected.Contains (Instance)) {
				value = result (Instance);
				return true;
			}
			return false;
		}

		public MatchSwitchArray (IMatchSwitch<Ingoing,Outgoing> inner, IEnumerable<Ingoing> expected, Func<Ingoing,Outgoing> result)
		{
			this.inner = inner;
			this.expected = expected.ToList();
			this.result = result;
		}
	}
	
}