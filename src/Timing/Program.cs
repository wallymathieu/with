using System;
using With;
using System.Linq;
using System.Collections.Generic;
using BenchmarkDotNet.Running;

namespace Timing
{

    class MainClass
    {

        public static void Main (string [] args)
        {
            switch (args.FirstOrDefault ()?.ToLowerInvariant ()) {
            case "clone": {
                    Console.WriteLine ("Starting: Same Class");
                    var summary = BenchmarkRunner.Run<TimingsClone> ();
                    break;
                }
            default: {
                    Console.WriteLine (@"Select one of 
new
clone
equal
");
                    break;
                }
            }
        }

    }
}
