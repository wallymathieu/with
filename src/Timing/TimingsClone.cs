using System;
using With;
using System.Collections.Generic;
namespace Timing
{
    class TimingsClone :TimingsBase
    {
        public TimingsClone():base(1000)
        {
            
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

            internal MyClass SetMyProperty2(string v)
            {
                return new MyClass(MyProperty, v);
            }
        }


        public void Timing_equalequal()
        {
            Do((i) =>
                {
                    var time = new DateTime(2001, 1, 1).AddMinutes(i);
                    new MyClass(1, "2").With(m => m.MyProperty2 == time.ToString());
                });
        }

        public void Timing_propertyname_only()
        {
            Do((i) =>
                {
                    var time = new DateTime(2001, 1, 1).AddMinutes(i);
                    new MyClass(1, "2").With(m => m.MyProperty2, time.ToString());
                });
        }
        public void Timing_dictionary()
        {
            Do((i) =>
                {
                    var time = new DateTime(2001, 1, 1).AddMinutes(i);
                    new MyClass(1, "2").With(new Dictionary<String, object> { { "MyProperty2", time.ToString() } });
                });
        }
        
        public void Timing_fluent()
        {
            Do((i) =>
            {
                var time = new DateTime(2001, 1, 1).AddMinutes(i);
                new MyClass(1, "2").With().Eql(p => p.MyProperty2, time.ToString())
                    .To(); // use to or cast to get the new instance
                });
        }

        public void Timing_by_hand()
        {
            Do((i) =>
                {
                    var time = new DateTime(2001, 1, 1).AddMinutes(i);
                    new MyClass(1, "2").SetMyProperty2(time.ToString());
                });
        }
    }

}
