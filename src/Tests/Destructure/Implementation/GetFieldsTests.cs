using Ploeh.AutoFixture.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using With;
using With.Destructure;
namespace Tests.Destructure.Implementation
{
    public class GetFieldsTests
    {
        public struct MyStruct0
        {
            public MyStruct0(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly string B;
            public readonly string C;
        }

        public class MyClass0
        {
            public MyClass0(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public readonly string A;
            public readonly string B;
            public readonly string C;
        }

        public class MyClass1
        {
            public MyClass1(string a, int b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
            public string A { get; private set; }
            public int B { get; private set; }
            public string C { get; private set; }
        }

        [Fact]
        public void Should_get_fields_of_class()
        {
            var result = new Fields(typeof(MyClass0), TypeOfFIelds.Fields).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }

        [Fact]
        public void Should_get_properties_of_class()
        {
            var result = new Fields(typeof(MyClass1), TypeOfFIelds.Properties).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }

        [Fact]
        public void Should_get_fields_of_struct()
        {
            var result = new Fields(typeof(MyStruct0), TypeOfFIelds.Fields).GetNames().ToArray();
            Assert.Equal(new[] { "A", "B", "C" }, result);
        }
    }
}
