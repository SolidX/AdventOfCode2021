using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_04
{
    public static class Extensions
    {
        public static bool IsBingoBoardRowString(this string str)
        {
            var spaces = str.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            ushort tmp;
            return spaces.Count() == 5 && spaces.All(n => UInt16.TryParse(n, out tmp));
        }

        public static IEnumerable<BingoBoardSpace> ToBingoBoardRow(this string str)
        {
            var spaces = str.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return spaces.Select(n => new BingoBoardSpace { Value = UInt16.Parse(n), Marked = false });
        }
    }
}
