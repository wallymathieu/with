using Ploeh.AutoFixture.Xunit2;
using Xunit;
using With.Destructure;
using With;
using System;
using System.Collections.Generic;
using System.Linq;
using With.Linq;

namespace Tests.Destructure
{
    public class DestructureListsTests
    {
        [Theory, AutoData]
        public void First_variable(
            int a, int b, int c)
        {
            Assert.Equal(a, new[] { a, b, c }.Let((x, _) => x));

            int result = -1;
            new[] { a, b, c }.Let((x, _) => { result = x; });
            Assert.Equal(a, result);
        }

        [Theory, AutoData]
        public void Second_variable(
            int a, int b, int c)
        {
            Assert.Equal(b, new[] { a, b, c }.Let((x, y, _) => y));

            int result = -1;
            new[] { a, b, c }.Let((x, y, _) => { result = y; });
            Assert.Equal(b, result);
        }

        [Fact]
        public void Yield_range()
        {
            var range = 0.To(4);
            var yielded = range.Pairwise((a, b) => new[] { a, b }).ToArray();
            Assert.Equal(new[] { new[] { 0, 1 }, new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 } }, yielded);
        }

        [Fact]
        public void GetNext()
        {
            var range = 0.To(1).GetEnumerator();
            Assert.Equal(0, range.GetNext());
            Assert.Equal(1, range.GetNext());
            Assert.Throws<OutOfRangeException>(()=> range.GetNext());
        }

        [Fact]
        public void Using_let_multiple_times_on_ienumerable()
        {
            var range = 0.To(5);
            var tuples = new List<Tuple<int, int>>();
            tuples.Add(range.Let((i, j, rest) => Tuple.Create(i, j)));
            tuples.Add(range.Let((i, j, rest) => Tuple.Create(i, j)));
            tuples.Add(range.Let((i, j, rest) => Tuple.Create(i, j)));
            Assert.Equal(Enumerable.Repeat(Tuple.Create(0, 1), 3).ToArray(), tuples.ToArray());
        }

        [Fact]
        public void Using_let_multiple_times_on_ienumerator()
        {
            var range = 0.To(5).GetEnumerator();
            var tuples = new List<Tuple<int, int>>();
            tuples.Add(range.Let((i, j) => Tuple.Create(i, j)));
            tuples.Add(range.Let((i, j) => Tuple.Create(i, j)));
            tuples.Add(range.Let((i, j) => Tuple.Create(i, j)));
            Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(2, 3), Tuple.Create(4,5) }, tuples.ToArray());

        }

    }
}
