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
            case "new": {
                    Console.WriteLine ("Starting: Class with additional property");
                    var summary = BenchmarkRunner.Run<TimingsNewClass> ();
                    break;
                }
            case "clone": {
                    Console.WriteLine ("Starting: Same Class");
                    var summary = BenchmarkRunner.Run<TimingsClone> ();
                    break;
                }
            case "equal": {
                    Console.WriteLine ("Starting: Struct Equals");
                    var summary = BenchmarkRunner.Run<TimingsEquals> ();
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
