using System;
using Xunit;
using With;
using Xunit.Extensions;
using Ploeh.AutoFixture.Xunit;
namespace Tests
{
    public class Another_instance_gets_created_without_the_need_for_inheritance
    {
        public struct CustomerFromSomeOtherDll 
        {
            private readonly int id;
            private readonly string name;
            private readonly DateTime since;
            public CustomerFromSomeOtherDll(int id, string name, DateTime since)
            {
                this.id = id;
                this.name = name;
                this.since = since;
            }
            public int Id { get { return id; } private set { throw new Exception(); } }
            public string Name { get { return name; } private set { throw new Exception(); } }
            public DateTime Since { get { return since; } private set { throw new Exception(); } }
        }

        [Theory, AutoData]
        public void A_class_should_map_properties(Customer myClass, DateTime time)
        {
            var ret = myClass.As<CustomerFromSomeOtherDll>(c=>c.Since==time);
            Assert.Equal(ret.Id, myClass.Id);
            Assert.Equal(ret.Name, myClass.Name);
            Assert.Equal(ret.Since, time);
        }
    }
}
