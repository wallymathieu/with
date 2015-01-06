using System;
using System.Collections.Generic;
using System.Linq;
namespace With
{
    internal class MapComparer
    {
        public static MapComparer<T,TComparable> Create<T,TComparable>(Func<T,TComparable> map)
            where TComparable: IComparable
        {
            return new MapComparer<T,TComparable>(map);
        }
    }

    internal class MapComparer<T,TComparable>:IComparer<T> where TComparable:IComparable
    {
        private readonly Func<T,TComparable> Map;
        private readonly IComparer<TComparable> Comparer = Comparer<TComparable>.Default;

        public MapComparer(Func<T,TComparable> map)
        {
            Map = map;
        }

        public int Compare(T x, T y)
        {
            return Comparer.Compare( Map(x), Map(y) );
        }
    }
}
