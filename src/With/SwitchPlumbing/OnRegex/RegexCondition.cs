namespace With.SwitchPlumbing
{
	public class RegexCondition : PreparedRegexCondition
    {
        private readonly string _string;

        public RegexCondition(string @string)
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