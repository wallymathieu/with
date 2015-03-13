using System;
using System.Collections.Generic;
using System.Linq;
using With.RangePlumbing;
namespace With.SwitchPlumbing
{
	public class Match<In,Out>:ISwitch<In,Out>{
		private readonly Func<In,bool> expected;
		private readonly Func<In,Out> result;
		private readonly ISwitch<In,Out> inner;
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

		public Match (ISwitch<In,Out> inner, IEnumerable<In> expected, Func<In,Out> result)
		{
			this.inner = inner;
            if (expected is IContainer<In>) {
                this.expected = ((IContainer<In>)(expected)).Contains;
			} else {
				this.expected = expected.Contains;
			}
			this.result = result;
		}
		public Match (ISwitch<In,Out> inner, Func<In,bool> expected, Func<In,Out> result)
		{
			this.inner = inner;
			this.expected = expected;
			this.result = result;
		}
	}
	
}