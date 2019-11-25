using System;
using With;
using With.Lenses;

namespace Tests.With.TestData
{
    public class Sale
    {
        private readonly int id;
        private readonly Customer customer;
        public Sale(int id, Customer customer)
        {
            this.id = id;
            this.customer = customer;
        }
        public int Id { get { return id; } private set { throw new Exception(); } }
        public Customer Customer { get { return customer; } private set { throw new Exception(); } }
        public static DataLens<Sale,Customer> _Customer = LensBuilder<Sale>.Of(c=>c.Customer).Build();
        public static DataLens<Sale,int> _Id = LensBuilder<Sale>.Of(c=>c.Id).Build();
    }
}
