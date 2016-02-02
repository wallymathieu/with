using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using With.Linq;

namespace With
{
    [Serializable]
    public class ExpectedButGotException : Exception
    {
        public ExpectedButGotException()
        {
        }

        public ExpectedButGotException(ExpressionType[] expected, ExpressionType got)
            : this(expected.Select(e => e.ToString()).ToArray(), got.ToString())
        {
        }

        public ExpectedButGotException(MemberTypes[] expected, MemberTypes got)
            : this(expected.Select(e => e.ToString()).ToArray(), got.ToString())
        {
        }

        public ExpectedButGotException(string[] expected, string got)
            : this(string.Format("Expected {0} but got {1}", string.Join(", ", expected), got))
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
