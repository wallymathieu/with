using System;

namespace With
{
	public static class CommonExtensions
	{
		public static T Tap<T>(this T that, Action<T> tapaction)
		{
			tapaction(that);
			return that;
		}

        public static TResult Yield<T, TResult>(this T self, Func<T, TResult> action)
        {
            return action(self);
        }
	}
}

