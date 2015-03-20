using System;
using System.Linq;

namespace Tests.Ranges.Adapters
{
    public class ConvertToRangeOfType<T> : IRangeConverter where T : IComparable, IComparable<T>
    {
        private static T C(object o)
        {
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public object[] ToArrayOf<SourceType>(SourceType[] array)
        {
            return array.Select(i => C(i)).Cast<object>().ToArray();
        }

        public IRange Range(object from, object to, object step)
        {
            return new RangeOfTypeAdapter<T>(@from, to, step);
        }
        public IRange Range(object from, object to)
        {
            return new RangeOfTypeAdapter<T>(@from, to);
        }
    }

}
