using System;
using Xunit.Extensions;
using System.Collections.Generic;
using System.Reflection;

namespace Tests.Interval.Adapters
{
    public class IntervalDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
        {
            return new List<object[]>{
                new object[]{ new ConvertToIntervalOfType<int>() },
                new object[]{ new ConvertToIntervalOfType<decimal>()},
                new object[]{ new ConvertToIntervalOfType<long>()}
            };
        }
    }
}
