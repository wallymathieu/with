namespace With.SwitchPlumbing
{
    public class RegexSwitchOnValue : PreparedRegexSwitch
    {
        private readonly string _string;

        public RegexSwitchOnValue(string @string)
        {
            _string = @string;
        }


        protected internal override bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }

		protected internal override string GetString()
        {
            return _string;
        }
    }
}