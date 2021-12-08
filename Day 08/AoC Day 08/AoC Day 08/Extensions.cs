using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_08
{
    public static class Extensions
    {
        public static IEnumerable<SevenSegmentDisplay> ToDisplays(this string str)
        {
            return str.Split(' ', StringSplitOptions.TrimEntries).Select(i => new SevenSegmentDisplay(i));
        }

        public static SevenSegmentDisplay Intersect(this SevenSegmentDisplay a, SevenSegmentDisplay b)
        {
            return new SevenSegmentDisplay
            {
                SegmentA = a.SegmentA && b.SegmentA,
                SegmentB = a.SegmentB && b.SegmentB,
                SegmentC = a.SegmentC && b.SegmentC,
                SegmentD = a.SegmentD && b.SegmentD,
                SegmentE = a.SegmentE && b.SegmentE,
                SegmentF = a.SegmentF && b.SegmentF,
                SegmentG = a.SegmentG && b.SegmentG
            };
        }

        public static SevenSegmentDisplay Exclude(this SevenSegmentDisplay a, SevenSegmentDisplay b)
        {
            return new SevenSegmentDisplay
            {
                SegmentA = a.SegmentA && !b.SegmentA,
                SegmentB = a.SegmentB && !b.SegmentB,
                SegmentC = a.SegmentC && !b.SegmentC,
                SegmentD = a.SegmentD && !b.SegmentD,
                SegmentE = a.SegmentE && !b.SegmentE,
                SegmentF = a.SegmentF && !b.SegmentF,
                SegmentG = a.SegmentG && !b.SegmentG
            };
        }
    }
}
