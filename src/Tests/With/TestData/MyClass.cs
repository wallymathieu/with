using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    public class MyClass
    {
        private readonly int myProperty;
        private readonly string myProperty2;
        public MyClass(int myProperty, string myProperty2)
        {
            this.myProperty = myProperty;
            this.myProperty2 = myProperty2;
        }
        public int MyProperty { get { return myProperty; } private set { throw new Exception(); } }
        public string MyProperty2 { get { return myProperty2; } private set { throw new Exception(); } }
    }
}
