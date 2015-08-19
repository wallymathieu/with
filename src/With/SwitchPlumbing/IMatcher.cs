using System;

namespace With.SwitchPlumbing
{
    public interface IMatcher<In,Out>
    {
        bool TryMatch(In instance, out Out value);
    }
}

