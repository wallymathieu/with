using System;

namespace With
{
	
	internal static class InternalExtensions
	{
		public static Int32 Plus(this Int32 that, Int32 other)
		{
			return that+other;
		}
		public static Int64 Plus(this Int64 that, Int64 other)
		{
			return that+other;
		}
		public static Decimal Plus(this Decimal that, Decimal other)
		{
			return that+other;
		}
		public static float Plus(this float that, float other)
		{
			return that+other;
		}
	}
}
