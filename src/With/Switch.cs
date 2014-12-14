using With.SwitchPlumbing;

namespace With
{
    public static partial class Switch
    {
        public static ISwitch<In, Nothing> Match<In>(In value)
        {
            return new Switch<In, Nothing>().Tap(c => c.Instance = value);
        }
        public static ISwitch<In, Nothing> On<In>(In value)
        {
            return new Switch<In, Nothing>().Tap(c => c.Instance = value);
        }


        public static ISwitch<In, Out> Match<In, Out>(In value)
        {
            return new Switch<In, Out>().Tap(c => c.Instance = value);
        }
        public static ISwitch<In, Out> On<In, Out>(In value)
        {
            return new Switch<In, Out>().Tap(c => c.Instance = value);
        }

        public static ISwitch<In, Prepared> Match<In>()
        {
            return new Switch<In, Prepared>();
        }
        public static ISwitch<In, Prepared> On<In>()
        {
            return new Switch<In, Prepared>();
        }

        public static ISwitch<In, Out> Match<In, Out>()
        {
            return new Switch<In, Out>();
        }
        public static ISwitch<In, Out> On<In, Out>()
        {
            return new Switch<In, Out>();
        }
    }
}
