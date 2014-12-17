using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.Destructure
{
    public class MatchFields<In,Out>
    {
        internal MatchFields()
        {
            Funcs = new List<Delegate>();
        }
        internal IList<Delegate> Funcs { get; private set; }
        public bool Fields { get; set; }
        public bool Properties { get; set; }
        public bool Methods { get; set; }
    }
}
