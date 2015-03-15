using Xunit;
using With.Collections;
using With;

namespace Tests.Collections
{
	public class IList_next_and_previous
	{
		[Fact]
		public void Next()
		{
			var collection = new int?[] { 0, 1, 2, 3, 4 };
			for (int i = 0; i < collection.Length - 1; i++)
			{
				Assert.Equal(i + 1, collection.Next(i));
			}
		}
		[Fact]
		public void Previous()
		{
			var collection = new int?[] { 0, 1, 2, 3, 4 };
			for (int i = collection.Length - 1; i >= 1; i--)
			{
				Assert.Equal(i - 1, collection.Previous(i));
			}
		}

		[Fact]
		public void Next_with_filter()
		{
			var collection = new int?[] { 0, 1, 2, 3, 4 };
			for (int i = 0; i < collection.Length - 1; i += 2)
			{
				Assert.Equal(i + 2, collection.Next(i, v => v % 2 == 0));
			}
		}
		[Fact]
		public void Previous_with_filter()
		{
			var collection = new int?[] { 0, 1, 2, 3, 4 };
			for (int i = collection.Length - 1; i >= 1; i -= 2)
			{
				Assert.Equal(i - 2, collection.Previous(i, v => v % 2 == 0));
			}
		}

		[Fact]
		public void Next_when_out_of_range()
		{
			var collection = new int?[] { 0, 1, 2, 3, 4 };
			Assert.Throws<OutOfRangeException>(() => collection.Next(collection.Length - 1));
			Assert.Equal(null, collection.Next(collection.Length - 1, valueWhenOutOfRange: _ => null));
		}

		[Fact]
		public void Previous_when_out_of_range()
		{
			var collection = new int?[] { 0, 1, 2, 3, 4 };
			Assert.Throws<OutOfRangeException>(() => collection.Previous(0));
			Assert.Equal(null, collection.Previous(0, valueWhenOutOfRange: _ => null));
		}
	}
}
