using Xunit;

namespace AoC_Day_18.Tests
{
    public class MagnitudeTests
    {
        [Fact]
        public void Test1()
        {
            var sn = InputParser.Parse("[[1,2],[[3,4],5]]");
            Assert.Equal(143, sn.Magnitude());
        }

        [Fact]
        public void Test2()
        {
            var sn = InputParser.Parse("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
            Assert.Equal(1384, sn.Magnitude());
        }

        [Fact]
        public void Test3()
        {
            var sn = InputParser.Parse("[[[[1,1],[2,2]],[3,3]],[4,4]]");
            Assert.Equal(445, sn.Magnitude());
        }

        [Fact]
        public void Test4()
        {
            var sn = InputParser.Parse("[[[[3,0],[5,3]],[4,4]],[5,5]]");
            Assert.Equal(791, sn.Magnitude());
        }

        [Fact]
        public void Test5()
        {
            var sn = InputParser.Parse("[[[[5,0],[7,4]],[5,5]],[6,6]]");
            Assert.Equal(1137, sn.Magnitude());
        }

        [Fact]
        public void Test6()
        {
            var sn = InputParser.Parse("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
            Assert.Equal(3488, sn.Magnitude());
        }
    }
}
