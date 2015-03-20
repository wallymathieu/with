using System;
using System.Collections.Generic;
namespace With.Collections
{
    public partial class Comparer
    {
        public static ComparerFromSelect<T, TComparable> Create<T, TComparable>(Func<T, TComparable> select)
            where TComparable : IComparable
        {
            return new ComparerFromSelect<T, TComparable>(select);
        }
    }

    public class ComparerFromSelect<T, TComparable> : IComparer<T> where TComparable : IComparable
    {
        private readonly Func<T, TComparable> Select;
        private readonly IComparer<TComparable> Comparer = Comparer<TComparable>.Default;

        public ComparerFromSelect(Func<T, TComparable> select)
        {
            Select = select;
        }

        public int Compare(T x, T y)
        {
            return Comparer.Compare(Select(x), Select(y));
        }
    }
}
