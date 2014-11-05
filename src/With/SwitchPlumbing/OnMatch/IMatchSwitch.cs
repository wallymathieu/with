using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public abstract class IMatchSwitch<Ingoing,Outgoing>
	{
		public abstract bool TryMatch (out Outgoing value);

		public abstract Ingoing Instance {
			get;
			set;
		}
		public static implicit operator Outgoing(IMatchSwitch<Ingoing,Outgoing> d)
		{
			Outgoing value;
			if (d.TryMatch (out value))
				return value;
			throw new Exception("Could not match");
		}

	}
}