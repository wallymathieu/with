using System;

namespace With
{
#if NOTCORE
    [Serializable]
#endif
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException() { }
        public OutOfRangeException(string message) : base(message) { }
        public OutOfRangeException(string message, Exception inner) : base(message, inner) { }
#if NOTCORE
        protected OutOfRangeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
#endif
    }
}
