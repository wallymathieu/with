using System;

namespace With
{
    /// <summary>
    /// Common extensions
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// Execute an action on the object. Return value is the object.
        /// </summary>
        public static T Tap<T>(this T value, Action<T> action)
        {
            action?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Execute a func on the object. Return value is the result from the func.
        /// </summary>
        public static TResult Yield<T, TResult>(this T value, Func<T, TResult> func) => func(value);
    }

}

