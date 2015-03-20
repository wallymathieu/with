using System.Collections.Generic;

namespace With
{
    public interface IRange<T> : IEnumerable<T>, IContainer<T> //where T: IComparable, IComparable<T>
    {
        IRange<T> Step(T step);
    }
}
