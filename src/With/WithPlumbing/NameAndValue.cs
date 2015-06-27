using System.Collections.Generic;
using System;
using System.Linq;

namespace With.WithPlumbing
{
    internal static class NameAndValues
    {
        public static KeyValuePair<string,object> Create(string name, object value)
        {
            return new KeyValuePair<string,object> (name, value);
        }
    }
}