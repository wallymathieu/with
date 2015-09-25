using System;

namespace With
{
    public class Interval<T> : IEquatable<Interval<T>>
        where T : IComparable, IComparable<T>
    {
        public T From { get; private set; }
        public T To { get; private set; }
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

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", From, To);
        }
    }
}
