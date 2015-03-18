using System;
namespace With.SwitchPlumbing
{
	public class MatchElse<In,Out>:ISwitch<In,Out>
	{
		private readonly Func<In,Out> result;
		private readonly ISwitch<In,Out> inner;

		public MatchElse (ISwitch<In,Out> inner,Func<In,Out> result)
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