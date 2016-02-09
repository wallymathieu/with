using System;
using System.Collections.Generic;
using Xunit;
using With;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;
using Tests.With.TestData;

namespace Tests
{
    public class A_new_instance_of_a_class_that_inherits_from_the_other
    {
        public class VipCustomer : Customer
        {
            private readonly DateTime since;
            public VipCustomer(int id, string name, IEnumerable<string> preferences, DateTime since)
                : base(id, name, preferences)
            {
                this.since = since;
            }

            public DateTime Since { get { return since; } private set { throw new Exception(); } }

        }

        [Theory, AutoData]
        public void A_class_should_map_its_parents_properties(
            Customer myClass, DateTime time)
        {
            var ret = myClass.As<VipCustomer>(c=>c.Since, time);
            Assert.Equal(time, ret.Since);

            Assert.Equal(myClass.Id, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_should_be_able_to_use_lambda(
            Customer myClass, DateTime time)
        {
            var ret = myClass.As<VipCustomer>(m => m.Since == time);
            Assert.Equal(ret.Since, time);

            Assert.Equal(myClass.Id, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_using_cast(
            Customer myClass, DateTime time)
        {
            Object _time = (Object)time;
            var ret = myClass.As<VipCustomer>(m => m.Since == (DateTime)_time);
            Assert.Equal(ret.Since, time);

            Assert.Equal(myClass.Id, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

        [Theory, AutoData]
        public void A_class_using_const(
    Customer myClass, DateTime time)
        {
            const string _time = "253654365";
            var ret = myClass.As<VipCustomer>(m => m.Name == _time && m.Since == time);
            Assert.Equal(ret.Name, _time);
        }

        [Theory, AutoData]
        public void A_class_should_map_its_parents_properties_and_get_the_new_value(
            Customer myClass, DateTime time)
        {
            VipCustomer ret = myClass.As<VipCustomer>()
                .Eql(p => p.Since, time);
            Assert.Equal(myClass.Id, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
            Assert.Equal(time, ret.Since);
        }

        public class MyCustomerWithDifferentParameterOrder : Customer
        {
            private readonly DateTime since;
            public MyCustomerWithDifferentParameterOrder(DateTime since, int id, IEnumerable<string> preferences, string name)
                : base(id, name, preferences)
            {
                this.since = since;
            }

            public DateTime Since { get { return since; } private set { throw new Exception(); } }
        }

        [Theory, AutoData]
        public void A_class_with_different_order_of_constructor_parameters(
            Customer myClass, DateTime time)
        {
            MyCustomerWithDifferentParameterOrder ret = myClass.As<MyCustomerWithDifferentParameterOrder>()
                .Eql(p => p.Since, time);
            Assert.Equal(time, ret.Since);

            Assert.Equal(myClass.Id, ret.Id);
            Assert.Equal(myClass.Name, ret.Name);
        }

    }
}
