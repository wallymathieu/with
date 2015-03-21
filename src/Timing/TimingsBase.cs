using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Timing
{
    abstract class TimingsBase
    {
        public IEnumerable<KeyValuePair<string, TimeSpan>> Get()
        {
            var methods = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => !method.GetParameters().Any())
                .Where(method => method.Name.StartsWith("Timing", StringComparison.InvariantCultureIgnoreCase));
            foreach (var method in methods)
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                method.Invoke(this, null);
                stopwatch.Stop();
                yield return new KeyValuePair<string, TimeSpan>(method.Name, stopwatch.Elapsed);
            }

        }
    }
}
