using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
#if NOTCORE
using System.Runtime.Serialization;
#endif
using With.Linq;

namespace With
{
    /// <summary>
    /// 
    /// </summary>
#if NOTCORE
    [Serializable]
#endif
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
#if NOTCORE
        protected ExpectedButGotException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
