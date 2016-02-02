using System;
namespace With
{
    [Serializable]
    public class NoMatchFoundException : Exception
    {
        public NoMatchFoundException() : base("Could not find a match. Are you missing a case or an else?") { }

        public NoMatchFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
        public NoMatchFoundException(string message): base(message)
        {
        }
        protected NoMatchFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
