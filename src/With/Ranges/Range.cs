using System;
using System.Collections;
using System.Collections.Generic;
using With.Ranges.Plumbing;

namespace With.Ranges
{
    /// <summary>
    /// Range represents a sequence of numeric or comparable values
    /// </summary>
    public class Range
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">need to be an int or decimal type</typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static Range<T> Create<T>(T @from, T @to, T step) where T : IComparable, IComparable<T>
        {
            return new Range<T>(@from, @to, step);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T> : IEnumerable<T>, IRange<T>
        where T : IComparable, IComparable<T>
    {
        private readonly IRange<T> inner;
        /// <summary>
        /// Create a new range with a default step of 1
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public Range(T @from, T @to)
            : this(@from, @to, (T)Convert.ChangeType(1, typeof(T)))
        {
        }
        /// <summary>
        /// Create a new range with a step
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        public Range(T @from, T @to, T step)
        {
            if (typeof(T) == typeof(Int32))
            {
                inner = (IRange<T>)new Int32Range(@from, @to, @step);
            }
            else if (typeof(T) == typeof(Int64))
            {
                inner = (IRange<T>)new Int64Range(@from, @to, @step);
            }
            else if (typeof(T) == typeof(Decimal))
            {
                inner = (IRange<T>)new DecimalRange(@from, @to, @step);
            }
            else
            {
                throw new Exception(String.Format("There is no implementation for type {0}", typeof(T).Name));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var i in inner)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        /// <summary>
        /// Get a new range with the same start and end, but with a different step between elements.
        /// </summary>
        public IRange<T> Step(T step)
        {
            return inner.Step(step);
        }
        /// <summary>
        /// Without enumerating this container, return true if the value is contained in the container.
        /// </summary>
        public bool Contains(T value)
        {
            return inner.Contains(value);
        }
    }
}

