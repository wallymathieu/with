using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public class MatchSwitchElse<In,Out>:IMatchSwitch<In,Out>
	{
		private readonly Func<In,Out> result;
		private readonly IMatchSwitch<In,Out> inner;

		public MatchSwitchElse (IMatchSwitch<In,Out> inner,Func<In,Out> result)
		{
			this.inner = inner;
			this.result = result;
		}
		public override bool TryMatch (out Out value)
		{
			if (inner.TryMatch (out value)) {
				return true;
			}
			value = result (Instance);
			return true;
		}

		public override In Instance {
			get{ return inner.Instance;}
			set{ inner.Instance = value;}
		}
	}
}