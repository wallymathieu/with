using System;

namespace With
{
#if NOTCORE
    [Serializable]
#endif
    public class MissingValueException : Exception
    {
        public MissingValueException() { }
        public MissingValueException(string message) : base(message) { }
        public MissingValueException(string message, Exception inner) : base(message, inner) { }
#if NOTCORE
        protected MissingValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
#endif
    }

}
