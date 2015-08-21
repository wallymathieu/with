using System;

namespace Timing
{
    /// <summary>
    /// This is not related to With, but tests of .net
    /// </summary>
    class TimingsEquals:TimingsBase
    {
        public TimingsEquals():base(10000000)
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


        public void Timing_equals_and_get_has_code_plain()
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

        public void Timing_equals_and_get_has_code_overridden()
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
    }
}

