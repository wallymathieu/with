using System;
using System.Collections.Generic;

namespace With.SwitchPlumbing
{
	public interface IMatchSwitch<Ingoing, Outgoing>:IMatchSwitch<Ingoing>{
		bool TryMatch (out Outgoing value);
	}
	
}