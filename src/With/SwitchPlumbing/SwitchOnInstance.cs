namespace With.SwitchPlumbing
{
    public class SwitchOnInstance : SwitchOn
    {
        private readonly object _instance;

        public SwitchOnInstance(object instance)
        {
            _instance = instance;
        }

        protected internal override bool TryGetValue(out object value)
        {
            value = null;
            return false;
        }

        protected internal override object GetInstance()
        {
            return _instance;
        }
    }
}