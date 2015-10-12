using System;
using With;
using System.Linq;
using System.Collections.Generic;
#if MARKDOWNLOG
using MarkdownLog;
#endif
namespace Timing
{
    #if! MARKDOWNLOG
    using System.Reflection;
    static class Mardowns{
        public static string ToMarkdownTable<T>(this IEnumerable<T> self){
            var properties = typeof(T).GetProperties()
                .Where(p=>p.CanRead && p.MemberType == MemberTypes.Property)
                .ToArray();
            var header = string.Join("|\t", properties.Select(p => p.Name).ToArray());
            return header + "\n" + string.Join("\n", self.Select(v => MapProperties(properties, v)));
        }
        private static string MapProperties(PropertyInfo[] properties,Object v){
            return String.Join("|\t", properties.Select(p=>p.GetValue(v,null)));
        }
    } 
    #endif

    class MainClass
    {

        public static void Main(string[] args)
        {
            {
                Console.WriteLine("Starting: Class with additional property");
                var m = new TimingsNewClass();
                var timings = m.Get().OrderBy(t => t.Value).ToArray();
                var low = timings.Min(t => t.Value);
                Console.WriteLine(timings.Select(t => Stat.Map(t, low)).ToMarkdownTable());
            }
            {
                Console.WriteLine("Starting: Same Class");
                var m = new TimingsClone();
                var timings2 = m.Get().OrderBy(t => t.Value).ToArray();
                var low = timings2.Min(t => t.Value);
                Console.WriteLine(timings2.Select(t => Stat.Map(t, low)).ToMarkdownTable());
            }
            {
                Console.WriteLine("Starting: Struct Equals");
                var m = new TimingsEquals();
                var timings2 = m.Get().OrderBy(t => t.Value).ToArray();
                var low = timings2.Min(t => t.Value);
                Console.WriteLine(timings2.Select(t => Stat.Map(t, low)).ToMarkdownTable());
            }
        }
            
        class Stat
        {
            public static Stat Map(KeyValuePair<string,TimeSpan> method, TimeSpan low){
                return new Stat
                {   Name = String.Join(" ",method.Key.Split('_')),
                    Elapsed = method.Value.ToString(), 
                    Quotient = (method.Value.Ticks / low.Ticks).ToString()
                };
            }
            public string Name { get; set;}
            public string Elapsed { get; set;}
            public string Quotient { get; set;}
        }
    }
}
