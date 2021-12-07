using System;
using System.Collections;
using System.Linq;

namespace AoC_Day_03
{
    public static class Extensions
    {
        public static long ToInt32(this BitArray x)
        {
            var value = 0L;
            for (var i = 0; i < x.Length; i++)
            {
                if (x[i])
                    value += (long)Math.Pow(2, 31 - i);
            }
            return value;
        }

        public static BitArray ToBitArray(this string s)
        {
            var uniqueChars = s.ToHashSet();
            if ((uniqueChars.Contains('1') || uniqueChars.Contains('0')) && uniqueChars.Count() <= 2)
            {
                
                var padding = new bool[32 - s.Length];
                var mask = s.Select(x => x == '1');
                return new BitArray(padding.Concat(mask).ToArray());
            }
            throw new ArgumentException();
        }
    }
}
