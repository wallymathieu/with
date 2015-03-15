using System;

namespace With
{
	[Serializable]
	public class OutOfRangeException : Exception
	{
		public OutOfRangeException() { }
		public OutOfRangeException(string message) : base(message) { }
		public OutOfRangeException(string message, Exception inner) : base(message, inner) { }
		protected OutOfRangeException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{ }
	}
}
