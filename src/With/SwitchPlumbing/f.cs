using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.SwitchPlumbing
{
    internal class F
    {
        protected internal static Func<Input, Return> ReturnDefault<Input, Return>(Action<Input> action)
        {
            return (incoming) =>
            {
                action(incoming);
                return default(Return);
            };
        }
        protected internal static Func<Input, Return> IgnoreInput<Input, Return>(Func<Return> result)
        {
            return ingoing => result();
        }
        protected internal static Action<Ingoing> IgnoreInput<Ingoing>(Action result)
        {
            return ingoing => result();
        }
    }
}
