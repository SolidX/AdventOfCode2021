using System.Collections.Generic;
using System.Text.Json;

namespace AoC_Day_18
{
    public static class InputParser
    {
        public static List<SnailfishNumber> Parse(string[] input)
        {
            var output = new List<SnailfishNumber>(input.Length);

            foreach (var i in input)
            {
                output.Add(Parse(i));
            }

            return output;
        }

        public static SnailfishNumber Parse(string input)
        {
            using (var doc = JsonDocument.Parse(input))
            {
                return new SnailfishNumber(doc.RootElement);
            }
        }
    }
}
