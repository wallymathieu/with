using With.SwitchPlumbing;

namespace With
{
    public class Switch
    {
        public static SwitchOnInstance On(object instance)
        {
            return new SwitchOnInstance(instance);
        }

        public static RegexSwitchValue Regex(string instance)
        {
            return new RegexSwitchValue(instance);
        }

        public static PreparedRegexSwitch Regex()
        {
            return new PreparedRegexSwitch();
        }


        public static PreparedSwitch On()
        {
            return new PreparedSwitch();
        }
    }
}
