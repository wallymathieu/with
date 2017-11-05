using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Tests.Intervals.Adapters
{
    public class IntervalDataAttribute : DataAttribute
    {
        public override IEnumerable<object []> GetData (MethodInfo testMethod)
        {
            return new List<object []>{
                new object[]{ new ConvertToIntervalOfType<int>() },
                new object[]{ new ConvertToIntervalOfType<decimal>()},
                new object[]{ new ConvertToIntervalOfType<long>()},
                new object[]{ new ConvertToIntervalOfType<DateTime>()}
            };
        }
    }
}
