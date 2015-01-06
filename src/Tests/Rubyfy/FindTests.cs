using System;
using With.Rubyfy;
using Xunit;
using System.Collections.Generic;

namespace Tests
{
    public class FindTests
    {
        [Fact]
        public void Can_find_index_of(){
            var array = new []{1,2,3,4,5,6 };
            Assert.Equal(0, array.FindIndex(1));
            Assert.Equal(0, array.FindIndex(i=>i==1));

            Assert.Equal(5, array.FindIndex(6));
            Assert.Equal(5, array.FindIndex(i=>i==6));
        }

        [Fact]
        public void Can_find_index_of_list(){
            var array = new List<int>{1,2,3,4,5,6 };
            Assert.Equal(0, array.FindIndex(1));
            Assert.Equal(0, array.FindIndex(i=>i==1));

            Assert.Equal(5, array.FindIndex(6));
            Assert.Equal(5, array.FindIndex(i=>i==6));
        }
    }
}

