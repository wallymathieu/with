using System;
using Xunit;
using With.Rubyfy;
using System.Collections.Generic;


namespace Tests
{
    public class FlatMapTests
    {
        [Fact]
        public void SimpleReturn(){
            var array = new []{ 1, 2, 3, 4 };
            Assert.Equal(new []{ 1, -1, 2, -2, 3, -3, 4, -4 }, array.FlatMap(e => new []{ e, -e }).ToA());
        }

        [Fact]
        public void EnumerablePlus(){
            var array = new []{ new []{1, 2}, new []{3, 4} };
            //throw new Exception(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            Assert.Equal(new []{ 1, 2, 100,  3, 4, 100 }, array.FlatMap<int>(e => new []{e, 100}).ToA());
        }

    }
}

