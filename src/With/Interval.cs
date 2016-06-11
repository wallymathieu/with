using System;

namespace With
{
    /// <summary>
    /// This is an inclusive interval. The end is contained in the interval. 
    /// The type T need to be comparable in order to be able to be able to determine if an element is contained in the interval.
    /// </summary>
    public class Interval<T> : IEquatable<Interval<T>>
        where T : IComparable, IComparable<T>
    {
        /// <summary>
        /// The start of the interval, the first element
        /// </summary>
        public T From { get; private set; }
        /// <summary>
        /// The end of the interval, the last element
        /// </summary>
        public T To { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Interval(T @from, T @to)
        {
            if (ReferenceEquals(@from, null)) { throw new NullReferenceException("from"); }
            if (ReferenceEquals(@to, null)) { throw new NullReferenceException("to"); }
            From = @from;
            To = @to;
        }

        internal T Contains(IComparable comparable)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Contains(T value)
        {
            if ( From.CompareTo(To) <= 0)
            {
                return From.CompareTo(value)  <= 0 && value.CompareTo(To) <= 0;
            }
            return To.CompareTo(value) <= 0 && value.CompareTo(From) <= 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Interval<T>);
        }

        public bool Equals(Interval<T> other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            return From.Equals(other.From)
                && To.Equals(other.To);
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() ^ To.GetHashCode();
        }
        /// <summary>
        /// Returns an interval like repressentation of the contents:
        /// [From, To]
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0}, {1}]", From, To);
        }
    }
}
