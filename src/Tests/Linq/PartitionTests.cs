using Xunit;
using With.Linq;
using System.Linq;
namespace Tests.Linq
{
    public class PartitionTests
    {
        [Fact]
        public void can_partition_into_two_sets()
        {
            Assert.Equal(new[] { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5, 7 } },
                new int[] { 1,2,3,4,5,6,7}.Partition(num=>num%2==0).ToArray());
        }
        [Fact]
        public void when_only_true()
        {
            Assert.Equal(new[] { new int[] { 1,2,3 }, new int[0] },
                new int[] { 1,2,3}.Partition(num=>true).ToArray());
        }
        [Fact]
        public void when_only_false()
        {
            Assert.Equal(new[] { new int[0], new int[] { 1,2,3 } },
                new int[] { 1,2,3}.Partition(num=>false).ToArray());
        }
    }
}
