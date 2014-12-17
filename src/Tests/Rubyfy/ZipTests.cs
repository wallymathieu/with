using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using With;
using With.Rubyfy;
namespace Tests.Rubyfy
{
    public class ZipTests
    {
        private readonly int?[] a = new int?[] { 4, 5, 6 };
        private readonly int?[] b = new int?[] { 7, 8, 9 };

        [Fact]
        public void test_zip_array_array()
        {
            Assert.Equal(new[] { new int?[] { 1, 4, 7 }, new int?[] { 2, 5, 8 }, new int?[] { 3, 6, 9 } },
                new int?[] { 1, 2, 3 }.Zip(a, b).ToA());
        }

        [Fact]
        public void test_zip_smaller_array()
        {
            Assert.Equal(new[] { new int?[] { 1, 4, 7 }, new int?[] { 2, 5, 8 } },
                new int?[] { 1, 2 }.Zip(a, b).ToA());
        }

        [Fact]
        public void test_zip_2()
        {
            Assert.Equal(new[] { new int?[] { 4, 1, 8 }, new int?[] { 5, 2, null }, new int?[] { 6, null, null } },
                a.Zip(new int?[] { 1, 2 }, new int?[] { 8 }).ToA());
        }
    }
}
