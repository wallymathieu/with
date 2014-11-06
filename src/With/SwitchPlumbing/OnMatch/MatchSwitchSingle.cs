using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public class MatchSwitchSingle<In,Out>:IMatchSwitch<In,Out>{
		private readonly In expected;
		private readonly Func<In,Out> result;
		private readonly IMatchSwitch<In,Out> inner;
		public override In Instance {
			get{ return inner.Instance;}
			set{ inner.Instance = value;}
		}
		public override bool TryMatch (out Out value)
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
		public MatchSwitchSingle (IMatchSwitch<In,Out> inner, In expected, Func<In,Out> result)
		{
			this.inner = inner;
			this.expected = expected;
			this.result = result;
		}

	}
}