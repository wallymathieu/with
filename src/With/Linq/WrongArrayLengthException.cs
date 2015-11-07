using System;
using System.Runtime.Serialization;

namespace With
{
    [Serializable]
    public class WrongArrayLengthException : Exception
    {
        public WrongArrayLengthException(int was, int expectedAtLeast)
        {
            this.Was= was;
            this.ExpectedAtLeast= expectedAtLeast;
        }

        public int ExpectedAtLeast { get; private set; }
        public int Was { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Was", Was);
            info.AddValue("ExpectedAtLeast", ExpectedAtLeast);

            base.GetObjectData(info, context);
        }
    }
}

