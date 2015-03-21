using System;

namespace With.Reflection
{
    [Flags]
    internal enum TypeOfFIelds
    {
        None = 0,
        Fields = 1,
        Properties = 2,
        Methods = 4
    }
}
