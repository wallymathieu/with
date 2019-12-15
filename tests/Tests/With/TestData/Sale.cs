using System;
using System.Collections.Generic;
using With;
using With.Lenses;

namespace Tests.With.TestData
{
    public class Sale
    {
        private readonly int id;
        private readonly Customer customer;
        private Product product;
        public Sale(int id, Customer customer, Product product)
        {
            this.id = id;
            this.customer = customer;
            this.product = product;
        }
        public int Id { get { return id; } private set { throw new Exception(); } }
        public Customer Customer { get { return customer; } private set { throw new Exception(); } }
        public Product Product { get { return product; } private set { throw new Exception(); } }
        public static DataLens<Sale,Customer> _Customer = LensBuilder<Sale>.Of(c=>c.Customer).Build();
        public static DataLens<Sale,Product> _Product = LensBuilder<Sale>.Of(c=>c.Product).Build();
        public static DataLens<Sale,int> _Id = LensBuilder<Sale>.Of(c=>c.Id).Build();
    }
}
