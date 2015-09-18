using System.Collections.Generic;
namespace With.Linq
{
    public class Partition<T> : IEnumerable<IEnumerable<T>>
    {
        public IEnumerable<T> True { get; private set; }
        public IEnumerable<T> False { get; private set; }
        internal Partition(IEnumerable<T> trueArray, IEnumerable<T> falseArray)
        {
            True = trueArray;
            False = falseArray;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return new List<IEnumerable<T>> { True, False }.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new[] { True, False }.GetEnumerator();
        }
    }
}

