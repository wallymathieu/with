using System.Collections.Generic;
namespace With.Rubyfy
{
    public class MinMaxPartition<T> : IEnumerable<IEnumerable<T>>
    {
        public IEnumerable<T> Min { get; private set; }
        public IEnumerable<T> Max { get; private set; }
        internal MinMaxPartition(IEnumerable<T> minArray, IEnumerable<T> maxArray)
        {
            Min = minArray;
            Max = maxArray;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return new List<IEnumerable<T>> { Min, Max }.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new[] { Min, Max }.GetEnumerator();
        }
    }
}
