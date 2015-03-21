using System;
using With;
using System.Linq;
namespace Timing
{

    class MainClass
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting: Class with additional property");
            var m = new TimingsNewClass();
            Console.WriteLine("Elapsed\t\t\t\tName");
            var timings = m.Get().OrderBy(t => t.Value);
            foreach (var method in timings)
            {
                Console.WriteLine("{0}\t\t{1}", method.Value.ToString(), method.Key);
            }

            Console.WriteLine("Starting: Same Class");
            var m2 = new TimingsClone();
            Console.WriteLine("Elapsed\t\t\t\tName");
            var timings2 = m2.Get().OrderBy(t => t.Value);
            foreach (var method in timings2)
            {
                Console.WriteLine("{0}\t\t{1}", method.Value.ToString(), method.Key);
            }
        }
    }
}
