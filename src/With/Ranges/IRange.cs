using System.Collections.Generic;

namespace With.Ranges
{
    /// <summary>
    /// Represents a range, where the collection need not be enumerated.
    /// </summary>
    /// <typeparam name="T">Is an integer or decimal.</typeparam>
    public interface IRange<T> : IEnumerable<T>, IContainer<T> //where T: IComparable, IComparable<T>
    {
        /// <summary>
        /// Get a new range with the same start and end, but with a different step between elements.
        /// </summary>
        IRange<T> Step(T step);
    }
}
