using System;

namespace Tests.With.TestData
{
    public class FlyFishingBuddy<TCustomer>
    {
        public FlyFishingBuddy(TCustomer customer, DateTime whenToGoFishing)
        {
            Customer = customer;
            WhenToGoFishing = whenToGoFishing;
        }
        public readonly TCustomer Customer;
        public readonly DateTime WhenToGoFishing;

    }
}
