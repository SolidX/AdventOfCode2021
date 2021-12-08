using System;
using System.Linq;

namespace AoC_Day_08
{
    public class SevenSegmentDisplay
    {
        public bool SegmentA { get; set; }
        public bool SegmentB { get; set; }
        public bool SegmentC { get; set; }
        public bool SegmentD { get; set; }
        public bool SegmentE { get; set; }
        public bool SegmentF { get; set; }
        public bool SegmentG { get; set; }

        public SevenSegmentDisplay()
        {
        }

        public SevenSegmentDisplay(string activatedSegments)
        {
            var chars = activatedSegments.ToHashSet();

            if (chars.Contains('a')) SegmentA = true;
            if (chars.Contains('b')) SegmentB = true;
            if (chars.Contains('c')) SegmentC = true;
            if (chars.Contains('d')) SegmentD = true;
            if (chars.Contains('e')) SegmentE = true;
            if (chars.Contains('f')) SegmentF = true;
            if (chars.Contains('g')) SegmentG = true;
        }

        public uint ActivatedSegmentCount()
        {
            var count = 0u;
            if (SegmentA) count++;
            if (SegmentB) count++;
            if (SegmentC) count++;
            if (SegmentD) count++;
            if (SegmentE) count++;
            if (SegmentF) count++;
            if (SegmentG) count++;

            return count;
        }

        public override bool Equals(object obj)
        {
            if (obj is SevenSegmentDisplay)
            {
                var b = (SevenSegmentDisplay)obj;
                return  (SegmentA == b.SegmentA) &&
                        (SegmentB == b.SegmentB) &&
                        (SegmentC == b.SegmentC) && 
                        (SegmentD == b.SegmentD) && 
                        (SegmentE == b.SegmentE) &&
                        (SegmentF == b.SegmentF) &&
                        (SegmentG == b.SegmentG);
            }
            return false;
        }

        public override string ToString()
        {
            //Free visualization because why not
            var output = new char[7][] {
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[6] { ' ', ' ', ' ', ' ', ' ', ' ' }
            };

            if (SegmentA)
                output[0] = new char[6] { ' ', '*', '*', '*', '*', ' ' };
            if (SegmentD)
                output[3] = new char[6] { ' ', '*', '*', '*', '*', ' ' };
            if (SegmentG)
                output[6] = new char[6] { ' ', '*', '*', '*', '*', ' ' };
            if (SegmentB)
            {
                output[1][0] = '*';
                output[2][0] = '*';
            }
            if (SegmentC)
            {
                output[1][5] = '*';
                output[2][5] = '*';
            }
            if (SegmentE)
            {
                output[4][0] = '*';
                output[5][0] = '*';
            }
            if (SegmentF)
            {
                output[4][5] = '*';
                output[5][5] = '*';
            }

            var strs = output.Select(x => new string(x) + Environment.NewLine);
            return String.Concat(strs);
        }
    }
}
