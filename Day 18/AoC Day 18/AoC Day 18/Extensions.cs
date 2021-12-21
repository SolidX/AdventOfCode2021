using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_18
{
    public static class Extensions
    {
        public static SnailfishNumber Sum(this ICollection<SnailfishNumber> source)
        {
            if (source.Count == 0) return null;
            if (source.Count == 1) return source.First();

            SnailfishNumber sum = null;
            foreach (var n in source)
            {
                if (sum == null)
                    sum = n;
                else
                    sum = sum + n;
            }
            return sum;
        }
    }
}
