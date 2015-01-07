using System;

namespace With
{
    public class WrongArrayLengthException:Exception
    {
        public WrongArrayLengthException(int was,int expectedAtLeast)
        {
            this.Data.Add("was", was);
            this.Data.Add("expectedAtLeast", expectedAtLeast);
        }
    }
}

