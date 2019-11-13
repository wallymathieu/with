using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    static class LazyT
    {
        public static Lazy<T> Create<T>(Func<T> func) => new Lazy<T>(func);
    }
}
