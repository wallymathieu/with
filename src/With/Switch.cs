using With.SwitchPlumbing;

namespace With
{
    public static partial class Switch
    {
		public static PreparedTypeSwitch On(object instance)
        {
			return new PreparedTypeSwitch().Tap(tc=>tc.Instance= instance);
        }
        public static PreparedTypeSwitch On()
        {
            return new PreparedTypeSwitch();
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
