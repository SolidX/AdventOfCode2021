using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_05
{
    public class Coordinate
    {
        public ushort X { get; set; }
        public ushort Y { get; set; }

        public Coordinate(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
