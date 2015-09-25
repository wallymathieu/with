using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace With.Collections
{
    /// <summary>
    /// not a functional construct, you might want to use
    /// <see cref="Destructure.IEnumerableExtensions.Stitch{T, TResult}(IEnumerable{T}, Func{T, T, TResult}, Func{T, TResult})"/>
    /// or
    /// <see cref="Destructure.IEnumerableExtensions.Stitch{T}(IEnumerable{T}, Action{T, T}, Action{T})"/>
    /// </summary>
    internal class PeekEnumerable<T> : IEnumerable<T>
    {
        public PeekEnumerable(IEnumerable<T> enumerable)
        {
            _buffer = enumerable.ToList();
        }
        private int _currentIndex = -1;
        private readonly List<T> _buffer;
        public T Current()
        {
            if (_currentIndex < _buffer.Count())
            {
                return _buffer[_currentIndex];
            }
            throw new ArgumentOutOfRangeException();
        }
        public bool HasMore() { return _currentIndex + 1 < _buffer.Count(); }
        public T Next()
        {
            _currentIndex++;
            return Current();
        }

        public bool TryPeek(out T value)
        {
            var idx = _currentIndex + 1;
            var canPeek = idx < _buffer.Count();
            value = canPeek ? _buffer[idx] : default(T);
            return canPeek;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _buffer.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
