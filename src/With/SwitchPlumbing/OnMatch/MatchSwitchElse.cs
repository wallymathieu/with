using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	
	public class MatchSwitchElse<Ingoing,Outgoing>:IMatchSwitch<Ingoing,Outgoing>
	{
		private readonly Func<Ingoing,Outgoing> result;
		private readonly IMatchSwitch<Ingoing,Outgoing> inner;

		public MatchSwitchElse (IMatchSwitch<Ingoing,Outgoing> inner,Func<Ingoing,Outgoing> result)
		{
			this.inner = inner;
			this.result = result;
		}
		public override bool TryMatch (out Outgoing value)
		{
			if (inner.TryMatch (out value)) {
				return true;
			}
			value = result (Instance);
			return true;
		}

		public override Ingoing Instance {
			get{ return inner.Instance;}
			set{ inner.Instance = value;}
		}
	}
}