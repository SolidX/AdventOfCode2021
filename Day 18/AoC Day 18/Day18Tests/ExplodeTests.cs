using Xunit;

namespace AoC_Day_18.Tests
{
    public class ExplodeTests
    {
        [Fact]
        public void Test1()
        {
            var sn = InputParser.Parse("[[[[[9,8],1],2],3],4]");
            var x = SnailfishNumber.Explodez(sn);
            sn.Left.Left.Left.Left.Explode();

            var correctResult = InputParser.Parse("[[[[0,9],2],3],4]");

            Assert.Equal(correctResult.ToString(), sn.ToString());
            Assert.Equal(correctResult.ToString(), x.ToString());
        }

        [Fact]
        public void Test2()
        {
            var sn = InputParser.Parse("[7,[6,[5,[4,[3,2]]]]]");
            var x = SnailfishNumber.Explodez(sn);
            sn.Right.Right.Right.Right.Explode();

            var correctResult = InputParser.Parse("[7,[6,[5,[7,0]]]]");

            Assert.Equal(correctResult.ToString(), sn.ToString());
            Assert.Equal(correctResult.ToString(), x.ToString());
        }

        [Fact]
        public void Test3()
        {
            var sn = InputParser.Parse("[[6,[5,[4,[3,2]]]],1]");
            var x = SnailfishNumber.Explodez(sn);
            sn.Left.Right.Right.Right.Explode();

            var correctResult = InputParser.Parse("[[6,[5,[7,0]]],3]");

            Assert.Equal(correctResult.ToString(), sn.ToString());
            Assert.Equal(correctResult.ToString(), x.ToString());
        }

        [Fact]
        public void Test4()
        {
            var sn = InputParser.Parse("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            var x = SnailfishNumber.Explodez(sn);
            sn.Left.Right.Right.Right.Explode();

            //the pair [3,2] is unaffected because the pair [7,3] is further to the left;
            //[3,2] would explode on the next action.
            var correctResult = InputParser.Parse("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");

            Assert.Equal(correctResult.ToString(), sn.ToString());
            Assert.Equal(correctResult.ToString(), x.ToString());
        }

        [Fact]
        public void Test5()
        {
            var sn = InputParser.Parse("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            var x = SnailfishNumber.Explodez(sn);
            sn.Right.Right.Right.Right.Explode();

            var correctResult = InputParser.Parse("[[3,[2,[8,0]]],[9,[5,[7,0]]]]");

            Assert.Equal(correctResult.ToString(), sn.ToString());
            Assert.Equal(correctResult.ToString(), x.ToString());
        }
    }
}
