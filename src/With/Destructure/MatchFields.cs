using System;
using System.Collections.Generic;

namespace With.Destructure
{
    using Reflection;
    public class MatchFields<In, Out>
    {
        internal MatchFields()
        {
            Funcs = new List<Delegate>();
        }
        internal IList<Delegate> Funcs { get; private set; }
        internal TypeOfFIelds TypeOfFields { get; set; }
        public bool Fields { set { TypeOfFields |= TypeOfFIelds.Fields; } }
        public bool Properties { set { TypeOfFields |= TypeOfFIelds.Properties; } }
        public bool Methods { set { TypeOfFields |= TypeOfFIelds.Methods; } }

        internal void Add(Delegate f)
        {
            Funcs.Add(f);
        }

    }
}
