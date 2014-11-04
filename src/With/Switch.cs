using With.SwitchPlumbing;

namespace With
{
    public class Switch
    {
		public static PreparedTypeSwitch On(object instance)
        {
			return new PreparedTypeSwitch().Tap(tc=>tc.SetInstance(instance));
        }

		public static PreparedRegexCondition Regex(string instance)
        {
			return new PreparedRegexCondition().Tap(c=>c.SetString(instance));
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
			return new MatchSwitch<Ingoing>().Tap(c=>c.SetValue(value));
		}

		public static MatchSwitch<Ingoing,Outgoing> Match<Ingoing,Outgoing> (Ingoing value)
		{
			return new MatchSwitch<Ingoing,Outgoing>().Tap(c=>c.SetValue(value));
		}
    }
}
