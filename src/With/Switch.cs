using With.SwitchPlumbing;

namespace With
{
    public static partial class Switch
    {
        public static IMatchSwitch<In, Nothing> On<In>(In value)
        {
            return new MatchSwitch<In, Nothing>().Tap(c => c.Instance = value);
		}
        public static IMatchSwitch<In, Prepared> On<In>()
        {
            return new MatchSwitch<In, Prepared>();
        }
        public static IMatchSwitch<object, Prepared> On()
        {
            return new MatchSwitch<object, Prepared>();
        }

        public static IMatchSwitch<In, Nothing> Match<In>(In value)
        {
            return new MatchSwitch<In, Nothing>().Tap(c => c.Instance = value);
        }

        public static IMatchSwitch<In, Out> Match<In, Out>(In value)
        {
            return new MatchSwitch<In, Out>().Tap(c => c.Instance = value);
        }

        public static IMatchSwitch<In, Prepared> Match<In>()
        {
            return new MatchSwitch<In, Prepared>();
        }

        public static IMatchSwitch<In, Out> Match<In, Out>()
        {
            return new MatchSwitch<In, Out>();
        }
    }
}
