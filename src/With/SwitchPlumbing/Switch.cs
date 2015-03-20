namespace With.SwitchPlumbing
{
    public class Switch<In, Out> : ISwitch<In, Out>
    {
        public override bool TryMatch(out Out value)
        {
            value = default(Out);
            return false;
        }

        public override In Instance
        {
            get;
            set;
        }

    }
}