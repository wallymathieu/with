using System;
using With;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
namespace Timing
{

	class MainClass
	{

		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting:");
			var m = new Timings ();
			Console.WriteLine ("Elapsed\t\t\t\tName");
			var timings = m.Get ().OrderBy(t=>t.Value);
			foreach (var method in timings) {
				Console.WriteLine ("{0}\t\t{1}",method.Value.ToString(), method.Key);
			}
		}

	}
}
