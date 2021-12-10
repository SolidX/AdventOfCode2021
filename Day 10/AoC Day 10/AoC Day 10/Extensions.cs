using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_10
{
    public static class Extensions
    {
        public static decimal Median(this IEnumerable<ulong> x)
        {
            var sorted = x.OrderBy(y => y).ToArray();
            if (sorted.Length % 2 == 1)
            {
                return sorted[sorted.Length / 2];
            }
            else
            {
                return ((decimal)sorted[sorted.Length / 2 - 1] + (decimal)sorted[sorted.Length / 2]) / 2m;
            }
        }
    }
}
