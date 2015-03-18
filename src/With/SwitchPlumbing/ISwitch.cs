namespace With.SwitchPlumbing
{
	public abstract class ISwitch<In,Out>
	{
		public abstract bool TryMatch (out Out value);

		public abstract In Instance {
			get;
			set;
		}

        public Out ValueOf(In instance)
        {
            this.Instance = instance;
            Out value;
            if (TryMatch(out value))
                return value;
            throw new NoMatchFoundException();
        }
        public Out Value(){ return ValueOf(Instance); }

		public static implicit operator Out(ISwitch<In,Out> d)
		{
            Out value;
            if (d.TryMatch(out value))
                return value;
            throw new NoMatchFoundException();
		}
	}
}