using Xunit;

namespace AoC_Day_18.Tests
{
    public class SplitTests
    {
        [Fact]
        public void Test1()
        {
            var sn = SnailfishNumber.Split(10);
            Assert.Equal("[5,5]", sn.ToString());
        }

        [Fact]
        public void Test2()
        {
            var sn = SnailfishNumber.Split(11);
            Assert.Equal("[5,6]", sn.ToString());
        }
    }
}
