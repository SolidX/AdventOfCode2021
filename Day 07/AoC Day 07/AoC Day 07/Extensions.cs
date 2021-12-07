using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_07
{
    public static class Extensions
    {
        public static decimal Median(this IEnumerable<ushort> x)
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
        public static IEnumerable<ushort> Mode(this IEnumerable<ushort> x)
        {
            var occurance = new Dictionary<ushort, ulong>();
            foreach (var n in x)
            {
                if (occurance.ContainsKey(n))
                    occurance[n]++;
                else
                    occurance.Add(n, 1);
            }

            var maxOccurances = occurance.Values.Max();
            return occurance.Where(kvp => kvp.Value == maxOccurances).Select(kvp => kvp.Key);
        }

        public static long TriangularSum(this int n)
        {
            //I figured there was a name for this sequence and Wiki knew it
            //https://en.wikipedia.org/wiki/Triangular_number

            return (long)n * ((long)n + 1L) / 2L;
        }
    }
}
