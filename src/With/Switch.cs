using With.SwitchPlumbing;

namespace With
{
    public class Switch
    {
        public static TypeSwitchOn On(object instance)
        {
            return new TypeSwitchOn(instance);
        }

        public static RegexCondition Regex(string instance)
        {
            return new RegexCondition(instance);
        }

		public static PreparedRegexCondition Regex()
        {
			return new PreparedRegexCondition();
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
