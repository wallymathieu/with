using System;

namespace Tests
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
    }
}
