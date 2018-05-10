using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Tests.Ranges.Adapters
{
    public class RangesDataAttribute : DataAttribute
    {
        public override IEnumerable<object []> GetData (MethodInfo testMethod)
        {
            return new List<object []>{
                new object[]{ new ConvertToRangeOfType<int>() },
                new object[]{ new ConvertToRangeOfType<decimal>()},
                new object[]{ new ConvertToRangeOfType<long>()}
            };
        }
    }
}
