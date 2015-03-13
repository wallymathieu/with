using System;
using System.Collections.Generic;
using System.Collections;
using With.RangePlumbing;

namespace With
{
    public interface IRange<T>: IEnumerable<T>, IContainer<T> //where T: IComparable, IComparable<T>
    {
        IRange<T> Step (T step);
    }    
}
