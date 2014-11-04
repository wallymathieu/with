namespace With.SwitchPlumbing
{
    public class TypeSwitchOn : TypeSwitchOnBase
    {
        private readonly object _instance;

        public TypeSwitchOn(object instance)
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