using System;
using Xunit;
using With.Rubyfy;
namespace With.Tests.Rubyfy
{
    public class GsubTests
    {
        [Fact]
        public void test_some_back_reference()
        {
            var expected = "First sentence. Second sentence.\nThird sentence!\nFourth sentence?\nFifth sentence.";
            var input = "First sentence. Second sentence.Third sentence!Fourth sentence?Fifth sentence.";
            Assert.Equal(expected, input.Gsub("/([.!?])([A-Z1-9])/", "\\1\n\\2"));
        }

        [Fact]
        public void test_should_not_interpret_thing_without_slashes_as_regex()
        {
            var input = "First sentence. Second sentence.Third sentence!Fourth sentence?Fifth sentence.";
            Assert.Equal(input, input.Gsub("([.!?])([A-Z1-9])", "\\1\n\\2"));
        }
    }
}
