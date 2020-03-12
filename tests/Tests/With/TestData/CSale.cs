using System;

namespace Tests.With.TestData
{
    public class CSale 
    {
        public bool FromMK { get; private set; }
        private readonly int id;
        private readonly CCustomer customer;
        private Product product;
        private CSale (int id, CCustomer customer, Product product)
        {
            this.id = id;
            this.customer = customer;
            this.product = product;
            FromMK = false;
        }
        public int Id { get { return id; } private set { throw new Exception (); } }
        public CCustomer Customer { get { return customer; } private set { throw new Exception (); } }
        public Product Product { get { return product; } private set { throw new Exception (); } }
        
        public static CSale MK(int id, CCustomer customer, Product product)
        {
            return new CSale (id, customer, product) { FromMK =true };
        }
    }
}
