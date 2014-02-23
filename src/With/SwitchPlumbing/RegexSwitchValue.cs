namespace With.SwitchPlumbing
{
    public class RegexSwitchValue : RegexSwitch
    {
        private readonly string _string;

        public RegexSwitchValue(string @string)
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