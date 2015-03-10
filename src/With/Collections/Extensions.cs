using System;
using System.Collections.Generic;
using With.Rubyfy;
namespace With.Collections
{
    public static class Extensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> self, T value)
        {
            var array = self.ToL();
            var retval = new T[array.Count + 1];
            array.CopyTo(retval);
            retval[array.Count] = value;
            return retval;
        }
    }
}

