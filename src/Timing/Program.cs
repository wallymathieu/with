using System;
using With;
using System.Linq;
namespace Timing
{

    class MainClass
    {

        public static void Main(string[] args)
        {
            {
                Console.WriteLine("Starting: Class with additional property");
                var m = new TimingsNewClass();
                Console.WriteLine(Format("Name", "Elapsed\t\t", "Elapsed / Fastest"));
                var timings = m.Get().OrderBy(t => t.Value).ToArray();
                var low = timings.Min(t => t.Value);
                foreach (var method in timings)
                {
                    Console.WriteLine(Format(method.Key, method.Value.ToString(), (method.Value.Ticks / low.Ticks).ToString()+"\t\t"));
                }
            }
            {
                Console.WriteLine("Starting: Same Class");
                var m = new TimingsClone();
                Console.WriteLine(Format("Name", "Elapsed\t\t", "Elapsed / Fastest"));
                var timings2 = m.Get().OrderBy(t => t.Value).ToArray();
                var low = timings2.Min(t => t.Value);
                foreach (var method in timings2)
                {
                    Console.WriteLine(Format(method.Key, method.Value.ToString(), (method.Value.Ticks / low.Ticks).ToString() + "\t\t"));
                }
            }
        }

        private static string Format(string name, params string[] columns)
        {
            return string.Format("{0}\t|\t{1}", string.Join("\t|\t", columns), name);
        }
    }
}
