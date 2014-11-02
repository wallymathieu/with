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
			var m = new MainClass ();
			var methods = m.GetType ().GetMethods (BindingFlags.Public | BindingFlags.Instance)
				.Where(method=>!method.GetParameters().Any())
				.Where(method=>method.Name.StartsWith("Timing",StringComparison.InvariantCultureIgnoreCase));
			foreach (var method in methods) {
				Stopwatch stopwatch = new Stopwatch();

				stopwatch.Start();
				method.Invoke (m, null);
				stopwatch.Stop ();
				Console.WriteLine ("Running {0} for {1}",method.Name,stopwatch.Elapsed.ToString());
			}
		}

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

		public class MyClass2 : MyClass
		{
			public DateTime MyProperty3 { get; set; }
			public MyClass2(int myProperty, string myProperty2, DateTime myProperty3)
				: base(myProperty, myProperty2)
			{
				MyProperty3 = myProperty3;
			}
		}

		public void Timing_equalequal()
		{
			for (int i = 0; i < 1000; i++)
			{
				var time = new DateTime(2001, 1, 1).AddMinutes(i);
				var ret = new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3 == time);
			}
		}
		public void Timing_propertyname_only()
		{
			for (int i = 0; i < 1000; i++)
			{
				var time = new DateTime(2001, 1, 1).AddMinutes(i);
				var ret = new MyClass(1, "2").As<MyClass2>(m => m.MyProperty3,time);
			}
		}
		public void Timing_dictionary()
		{
			for (int i = 0; i < 1000; i++)
			{
				var time = new DateTime(2001, 1, 1).AddMinutes(i);
				var ret = new MyClass(1, "2").As<MyClass2>(new Dictionary<String, object> { {"MyProperty3",time} });
			}
		}

		public void Timing_ordinal()
		{
			for (int i = 0; i < 1000; i++)
			{
				var time = new DateTime(2001, 1, 1).AddMinutes(i);
				var ret = new MyClass(1, "2").As<MyClass2>(time);
			}
		}

		public void Timing_fluent()
		{
			for (int i = 0; i < 1000; i++)
			{
				var time = new DateTime(2001, 1, 1).AddMinutes(i);
				var ret = new MyClass(1, "2").As<MyClass2>().Eql(p=>p.MyProperty3,time)
					.To(); // use to or cast to get the new instance
			}
		}
	}
}
