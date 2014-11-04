using System;
using System.Collections.Generic;
using System.Linq;
namespace With.SwitchPlumbing
{
	public interface IMatchSwitch<Ingoing>
	{
		bool TryMatch ();

		Ingoing Instance {
			get;
			set;
		}
	}
	
}