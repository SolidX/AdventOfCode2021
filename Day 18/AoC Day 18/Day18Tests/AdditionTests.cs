using Xunit;

namespace AoC_Day_18.Tests
{
    public class AdditionTests
    {
        [Fact]
        public void Test1()
        {
            var sn1 = InputParser.Parse("[1,2]");
            var sn2 = InputParser.Parse("[[3,4],5]");

            var sum = sn1 + sn2;
            Assert.Equal("[[1,2],[[3,4],5]]", sum.ToString());
        }

        [Fact]
        public void Test2()
        {
            var inputs = new string[] { "[1, 1]", "[2, 2]", "[3, 3]", "[4, 4]" };
            var sns = InputParser.Parse(inputs);

            var sum = sns[0] + sns[1] + sns[2] + sns[3];
            Assert.Equal("[[[[1,1],[2,2]],[3,3]],[4,4]]", sum.ToString());
        }

        [Fact]
        public void Test3()
        {
            var inputs = new string[] { "[1,1]", "[2,2]", "[3,3]", "[4,4]", "[5,5]" };
            var sns = InputParser.Parse(inputs);

            var sum = sns[0] + sns[1] + sns[2] + sns[3] + sns[4];

            Assert.Equal("[[[[3,0],[5,3]],[4,4]],[5,5]]", sum.ToString());
        }

        [Fact]
        public void Test4()
        {
            //Test driven development because literally nothing works
            var inputs = new string[]
            {
                "[[[0,[5, 8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
                "[[[5,[2,8]],4],[5,[[9,9],0]]]",
                "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
                "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
                "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
                "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
                "[[[[5,4],[7,7]],8],[[8,3],8]]",
                "[[9,3],[[9,9],[6,[4,9]]]]",
                "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
                "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
            };
            var sns = InputParser.Parse(inputs);

            var sum = sns.Sum();
            Assert.Equal("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", sum.ToString());
        }
    }
}
