using System;
using With;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
namespace Timing
{
    class TimingsClone
    {

        public class MyClass
        {
            public MyClass(int myProperty, string myProperty2)
            {
                MyProperty = myProperty;
                MyProperty2 = myProperty2;
            }
            public int MyProperty { get; private set; }
            public string MyProperty2 { get; private set; }
        }


        public void Timing_equalequal()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass>(m => m.MyProperty2 == time.ToString());
            }
        }

        public void Timing_propertyname_only()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass>(m => m.MyProperty2, time.ToString());
            }
        }
        public void Timing_dictionary()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass>(new Dictionary<String, object> { { "MyProperty2", time.ToString() } });
            }
        }
        
        public void Timing_fluent()
        {
            for (int i = 0; i < 1000; i++)
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").As<MyClass>().Eql(p => p.MyProperty2, time.ToString())
                    .To(); // use to or cast to get the new instance
            }
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
    }

}
