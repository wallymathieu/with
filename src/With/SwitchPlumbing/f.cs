using System;

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

        protected internal static Func<Input, TReturn> IgnoreInput<Input, TReturn>(TReturn value)
        {
            return (incoming) => { return value; };
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
