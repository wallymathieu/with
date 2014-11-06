using System;
using System.Collections.Generic;
using System.Linq;
using With.RangePlumbing;
namespace With.SwitchPlumbing
{
	public class MatchSwitchFunc<In,Out>:IMatchSwitch<In,Out>{
		private readonly Func<In,bool> expected;
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
			if (expected (Instance)) {
				value = result (Instance);
				return true;
			}
			return false;
		}

		public MatchSwitchFunc (IMatchSwitch<In,Out> inner, IEnumerable<In> expected, Func<In,Out> result)
		{
			this.inner = inner;
			if (expected is IStep<In>) {
				this.expected = ((IStep<In>)(expected)).Contain;
			} else {
				this.expected = expected.Contains;
			}
			this.result = result;
		}
		public MatchSwitchFunc (IMatchSwitch<In,Out> inner, Func<In,bool> expected, Func<In,Out> result)
		{
			this.inner = inner;
			this.expected = expected;
			this.result = result;
		}
	}
	
}