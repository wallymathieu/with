using System;

namespace With
{
    [Serializable]
    public class MissingValueException : Exception
    {
        public MissingValueException() { }
        public MissingValueException(string message) : base(message) { }
        public MissingValueException(string message, Exception inner) : base(message, inner) { }
        protected MissingValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    
}
