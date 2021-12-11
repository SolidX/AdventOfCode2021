using System.Collections.Generic;

namespace AoC_Day_11
{
    public class CoordinateEqualityComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }

        public int GetHashCode(Coordinate c)
        {
            return c.GetHashCode();
        }
    }
}
