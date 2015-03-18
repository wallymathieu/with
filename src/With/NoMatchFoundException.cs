using System;
namespace With
{
	[Serializable]
    public class NoMatchFoundException : Exception
    {
        public NoMatchFoundException() : base("Could not find a match. Are you missing a case or an else?") { }

        protected NoMatchFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}