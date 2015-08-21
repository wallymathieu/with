using System;

namespace Timing
{
    /// <summary>
    /// This is not related to With, but tests of .net
    /// </summary>
    class TimingsEquals:TimingsBase
    {
        public TimingsEquals():base(1000000)
        {
            
        }
        public struct CustomerInfo
        {
            public readonly string Name;
            public readonly int Age;
            public CustomerInfo(string name, int age)
            {
                Name=name;
                Age=age;
            }
        }


        public struct ProductInfo: IEquatable<ProductInfo>
        {
            public readonly string Name;
            public readonly int Price;
            public ProductInfo(string name, int price)
            {
                Name=name;
                Price=price;
            }

            public bool Equals(ProductInfo other)
            {
                return Name == other.Name && Price == other.Price;
            }
            public static bool operator ==(ProductInfo x, ProductInfo y) 
            {
                return x.Equals(y);
            }
            public static bool operator !=(ProductInfo x, ProductInfo y) 
            {
                return !x.Equals(y);
            }
            public override bool Equals(object obj)
            {
                if (obj is ProductInfo)
                    return Equals((ProductInfo)obj);
                else
                    return false;
            }
            public override int GetHashCode()
            {
                return Name.GetHashCode() ^ Price.GetHashCode();
            }
        }



        public class Product: IEquatable<Product>
        {
            public readonly string Name;
            public readonly int Price;
            public Product(string name, int price)
            {
                Name=name;
                Price=price;
            }

            public bool Equals(Product other)
            {
                return Name == other.Name && Price == other.Price;
            }
            public static bool operator ==(Product x, Product y) 
            {
                return x.Equals(y);
            }
            public static bool operator !=(Product x, Product y) 
            {
                return !x.Equals(y);
            }
            public override bool Equals(object obj)
            {
                if (obj is Product)
                    return Equals((Product)obj);
                else
                    return false;
            }
            public override int GetHashCode()
            {
                return Name.GetHashCode() ^ Price.GetHashCode();
            }
        }

        public class ProductContainer: IEquatable<ProductContainer>
        {
            public readonly ProductInfo Product;
            public ProductContainer(ProductInfo product)
            {
                Product=product;
            }

            public bool Equals(ProductContainer other)
            {
                return this.Product == other.Product;
            }
            public static bool operator ==(ProductContainer x, ProductContainer y) 
            {
                return x.Equals(y);
            }
            public static bool operator !=(ProductContainer x, ProductContainer y) 
            {
                return !x.Equals(y);
            }
            public override bool Equals(object obj)
            {
                if (obj is ProductContainer)
                    return Equals((ProductContainer)obj);
                else
                    return false;
            }
            public override int GetHashCode()
            {
                return Product.GetHashCode();
            }
        }
        public void Timing_equals_and_get_hash_code_plain()
        {
            var clone = new CustomerInfo("Test", 44);
            var oneFieldDifferent = new CustomerInfo("Test", 64);
            Do((i) =>
                {
                    var res = 
                        clone.Equals(oneFieldDifferent) &&
                        clone.GetHashCode() == oneFieldDifferent.GetHashCode();
                });
        }

        public void Timing_equals_and_get_hash_code_overridden()
        {
            var clone = new ProductInfo("Test", 44);
            var oneFieldDifferent = new ProductInfo("Test", 64);
            Do((i) =>
                {
                    var res = 
                        clone==oneFieldDifferent &&
                        clone.GetHashCode() == oneFieldDifferent.GetHashCode();
                });
        }

        public void Timing_equals_and_get_hash_code_overridden_in_class()
        {
            var clone = new Product("Test", 44);
            var oneFieldDifferent = new Product("Test", 64);
            Do((i) =>
                {
                    var res = 
                        clone==oneFieldDifferent &&
                        clone.GetHashCode() == oneFieldDifferent.GetHashCode();
                });
        }

        public void Timing_equals_and_get_hash_code_containing_struct()
        {
            var clone = new ProductContainer( new ProductInfo("Test", 44));
            var oneFieldDifferent = new ProductContainer( new ProductInfo("Test", 64));
            Do((i) =>
                {
                    var res = 
                        clone==oneFieldDifferent &&
                        clone.GetHashCode() == oneFieldDifferent.GetHashCode();
                });
        }
    }
}

