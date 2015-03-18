using System;
using System.Collections.Generic;

namespace Tests
{
	public class MyClass
    {
        private readonly int myProperty;
        private readonly string myProperty2;
        private readonly IEnumerable<string> myProperty3;
        public MyClass(int myProperty, string myProperty2, IEnumerable<string> myProperty3)
        {
            this.myProperty = myProperty;
            this.myProperty2 = myProperty2;
            this.myProperty3 = myProperty3;
        }
        public int MyProperty { get { return myProperty; } private set { throw new Exception(); } }
        public string MyProperty2 { get { return myProperty2; } private set { throw new Exception(); } }
        public IEnumerable<string> MyProperty3{ get { return myProperty3; } private set { throw new Exception(); } }
    }
}
