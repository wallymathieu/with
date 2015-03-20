using System;
using Xunit.Extensions;
using System.Collections.Generic;
using System.Reflection;

namespace Tests.Ranges.Adapters
{
    public class RangesDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
        {
            return new List<object[]>{
                new object[]{ new ConvertToRangeOfType<int>() },
                new object[]{ new ConvertToRangeOfType<decimal>()},
                new object[]{ new ConvertToRangeOfType<long>()}
            };
        }
    }
}
