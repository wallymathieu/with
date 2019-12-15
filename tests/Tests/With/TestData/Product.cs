using With;
using With.Lenses;

namespace Tests.With.TestData
{
    public class Product
    {
        public Product(int id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public int Id { get; }
        public decimal Price { get; }
        public static DataLens<Product,decimal> _Price = LensBuilder<Product>.Of(c=>c.Price).Build();

    }
}
