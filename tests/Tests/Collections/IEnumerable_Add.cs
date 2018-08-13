using Xunit;
using With;
using With.Ranges;
using System.Linq;
namespace Tests
{
    public class IEnumerable_Add
    {
        [Fact]
        public void Can_add_range()
        {
            var range = 0.To(3).ToList().Tap(l => l.AddRange((-1).To(-3)));
            Assert.Equal(new[] { 0, 1, 2, 3, -1, -2, -3 }, range.ToArray());
        }
    }
}

