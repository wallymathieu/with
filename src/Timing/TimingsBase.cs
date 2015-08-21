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
        private readonly int times;
        public TimingsBase(int times)
        {
            this.times = times;
        }

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
        public void Do(Action @do)
        {
            for (int i = 0; i < times; i++)
            {
                @do();
            }
        }
        public void Do(Action<int> @do)
        {
            for (int i = 0; i < times; i++)
            {
                @do(i);
            }
        }
    }
}
