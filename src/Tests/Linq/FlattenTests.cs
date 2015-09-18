using System.Collections;
using Xunit;
using With.Linq;
using System.Linq;

namespace With.Tests.Linq
{
    public class FlattenTests
    {
        [Fact]
        public void test_flatten()
        {
            var a1 = new[] { 1, 2, 3 };
            var a2 = new[] { 5, 6 };
            var a3 = new object[] { 4, a2 };
            var a4 = new object[] { a1, a3 };

            assert_equal(new[] { 1, 2, 3, 4, 5, 6 }, a4.Flatten());
            assert_equal(new object[] { a1, a3 }, a4);

            var a5 = new object[] { a1, new object[0], a3 };
            assert_equal(new object[] { 1, 2, 3, 4, 5, 6 }, a5.Flatten());
            assert_equal(new object[0], new object[0].Flatten());
            assert_equal(new object[0],
                           new object[] { new object[] { new object[] { new object[] { }, new object[] { } }, new object[] { new object[] { } }, new object[] { } }, new object[] { new object[] { new object[] { } } } }.Flatten());

            var a8 = new object[] { new object[] { 1, 2 }, 3 };
            var a9 = a8.Flatten(0);
            assert_equal(a8, a9);
        }

        [Fact]
        public void test_flatten_strings()
        {
            var a1 = new[] { "1", "2", "3" };
            var a2 = new[] { "5", "6" };
            var a3 = new object[] { "4", a2 };
            var a4 = new object[] { a1, a3 };

            assert_equal(new[] { "1", "2", "3", "4", "5", "6" }, a4.Flatten());
            assert_equal(new object[] { a1, a3 }, a4);

            var a5 = new object[] { a1, new object[0], a3 };
            assert_equal(new object[] { "1", "2", "3", "4", "5", "6" }, a5.Flatten());
            assert_equal(new object[0], new object[0].Flatten());
            assert_equal(new object[0],
                           new object[] { new object[] { new object[] { new object[] { }, new object[] { } }, new object[] { new object[] { } }, new object[] { } }, new object[] { new object[] { new object[] { } } } }.Flatten());

            var a8 = new object[] { new object[] { "1", "2" }, "3" };
            var a9 = a8.Flatten(0);
            assert_equal(a8, a9);

            assert_equal(new object[] { "abc" },
                new object[] { new object[] { new string[0] }, new[] { "abc" } }.Flatten());
            assert_equal(new object[] { "abc" },
                new object[] { new[] { "abc" }, new object[] { new string[0] } }.Flatten());

        }

        private void assert_equal(IEnumerable expected, IEnumerable actual)
        {
            Assert.Equal(expected.Cast<object>().ToArray(), actual.Cast<object>().ToArray());
        }
    }
}
