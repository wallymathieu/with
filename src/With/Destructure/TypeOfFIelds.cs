using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.Destructure
{
    [Flags]
    internal enum TypeOfFIelds
    {
        None=0,
        Fields = 1,
        Properties = 2,
        Methods = 4
    }
}
