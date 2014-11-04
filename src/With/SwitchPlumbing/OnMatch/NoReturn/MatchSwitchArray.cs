using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{

	public class MatchSwitchArray<Ingoing>:IMatchSwitch<Ingoing>{
		private readonly IList<Ingoing> expected;
		private readonly Action<Ingoing> result;
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
			if (expected.Contains (Instance)) {
				result (Instance);
				return true;
			}
			return false;
		}

		public MatchSwitchArray (IMatchSwitch<Ingoing> inner, IEnumerable<Ingoing> expected, Action<Ingoing> result)
		{
			this.inner = inner;
			this.expected = expected.ToList();
			this.result = result;
		}
	}
	
}