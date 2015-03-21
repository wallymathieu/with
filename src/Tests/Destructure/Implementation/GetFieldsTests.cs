using System.Linq;
using Xunit;
using With.Destructure;
using With.Reflection;
namespace Tests.Destructure.Implementation
{
    public class GetFieldsTests
    {
        public struct MyStructWithFields0
        {
            public MyStructWithFields0(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly string B;
            public readonly string C;
        }

        public class MyClassWithFIelds0
        {
            public MyClassWithFIelds0(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly string B;
            public readonly string C;
        }

        public class MyClassWithProperties1
        {
            public MyClassWithProperties1(string a, int b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public string A { get; private set; }
            public int B { get; private set; }
            public string C { get; private set; }
        }

        public struct MyStructWithProperties1
        {
            public MyStructWithProperties1(string a, int b, string c) : this()
            {
                A = a;
                B = b;
                C = c;
            }
            public string A { get; private set; }
            public int B { get; private set; }
            public string C { get; private set; }
        }

        public class MyClassWithMethods2
        {
            private readonly string a;
            private readonly int b;
            private readonly string c;
            public MyClassWithMethods2(string a, int b, string c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            public string GetA() { return a; }
            public int GetB() { return b; }
            public string GetC() { return c; }
        }


        public struct MyStructWithMethods2
        {
            private readonly string a;
            private readonly int b;
            private readonly string c;
            public MyStructWithMethods2(string a, int b, string c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            public string GetA() { return a; }
            public int GetB() { return b; }
            public string GetC() { return c; }
        }

        [Fact]
        public void Should_get_fields_of_class()
        {
            var result = new Fields(typeof(MyClassWithFIelds0), TypeOfFIelds.Fields).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }
        [Fact]
        public void Should_get_fields_of_struct()
        {
            var result = new Fields(typeof(MyStructWithFields0), TypeOfFIelds.Fields).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }


        [Fact]
        public void Should_get_properties_of_class()
        {
            var result = new Fields(typeof(MyClassWithProperties1), TypeOfFIelds.Properties).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }
        [Fact]
        public void Should_get_properties_of_struct()
        {
            var result = new Fields(typeof(MyStructWithProperties1), TypeOfFIelds.Properties).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }


        [Fact]
        public void Should_get_methods_of_class()
        {
            var result = new Fields(typeof(MyClassWithMethods2), TypeOfFIelds.Methods).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }
        [Fact]
        public void Should_get_methods_of_struct()
        {
            var result = new Fields(typeof(MyStructWithMethods2), TypeOfFIelds.Methods).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }
    }
}
