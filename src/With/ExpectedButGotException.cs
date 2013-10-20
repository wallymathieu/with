using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace With
{
    [Serializable]
    public class ExpectedButGotException : Exception
    {
        public ExpectedButGotException(ExpressionType[] expected, ExpressionType got)
            :this(string.Format("Expected {0} but got {1}", string.Join(", ",expected), got))
        {
        }

        public ExpectedButGotException(string message)
            : base(message)
        {
        }

        public ExpectedButGotException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ExpectedButGotException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
