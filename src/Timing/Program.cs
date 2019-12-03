using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace Timing
{

    class MainClass
    {

        public static void Main(string[] args)
        {
            switch (args.FirstOrDefault()?.ToLowerInvariant())
            {
                case "single":
                    {
                        var summary = BenchmarkRunner.Run<Copy_update_single_property>();
                        break;
                    }
                case "three":
                    {
                        var summary = BenchmarkRunner.Run<Lens_with_three_properties>();
                        break;
                    }
                default:
                    {
                        Console.WriteLine(@"Select one of 
single
three
");
                        break;
                    }
            }
        }

    }
}
