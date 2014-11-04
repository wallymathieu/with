using With.SwitchPlumbing;

namespace With
{
    public class Switch
    {
        public static TypeSwitchOnInstance On(object instance)
        {
            return new TypeSwitchOnInstance(instance);
        }

        public static RegexSwitchOnValue Regex(string instance)
        {
            return new RegexSwitchOnValue(instance);
        }

        public static PreparedRegexSwitch Regex()
        {
            return new PreparedRegexSwitch();
        }


        public static PreparedTypeSwitch On()
        {
            return new PreparedTypeSwitch();
        }

		public static MatchSwitch<Ingoing> Match <Ingoing>(Ingoing value)
		{
			return new MatchSwitch<Ingoing>();
		}

		public static MatchSwitch<Ingoing,Outgoing> Match<Ingoing,Outgoing> (Ingoing v)
		{
			throw new System.NotImplementedException ();
		}
    }
}
