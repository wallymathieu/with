using System;

namespace With
{
    [Serializable]
    public class ShouldBeAnExpressionLeftToRightException : Exception
    {
        public ShouldBeAnExpressionLeftToRightException() { }
        public ShouldBeAnExpressionLeftToRightException(string message) : base(message) { }
        public ShouldBeAnExpressionLeftToRightException(string message, Exception inner) : base(message, inner) { }
        protected ShouldBeAnExpressionLeftToRightException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
