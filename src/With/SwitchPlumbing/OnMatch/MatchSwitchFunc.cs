using System;
using System.Collections.Generic;
using System.Linq;
using With.RangePlumbing;
namespace With.SwitchPlumbing
{
	public class MatchSwitchFunc<Ingoing,Outgoing>:IMatchSwitch<Ingoing,Outgoing>{
		private readonly Func<Ingoing,bool> expected;
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
			if (expected (Instance)) {
				value = result (Instance);
				return true;
			}
			return false;
		}

		public MatchSwitchFunc (IMatchSwitch<Ingoing,Outgoing> inner, IEnumerable<Ingoing> expected, Func<Ingoing,Outgoing> result)
		{
			this.inner = inner;
			if (expected is IStep<Ingoing>) {
				this.expected = ((IStep<Ingoing>)(expected)).Contain;
			} else {
				this.expected = expected.ToList ().Contains;
			}
			this.result = result;
		}
		public MatchSwitchFunc (IMatchSwitch<Ingoing,Outgoing> inner, Func<Ingoing,bool> expected, Func<Ingoing,Outgoing> result)
		{
			this.inner = inner;
			this.expected = expected;
			this.result = result;
		}
	}
	
}