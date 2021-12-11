using System;

namespace AoC_Day_11
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public bool Equals(Coordinate b)
        {
            if (Object.ReferenceEquals(b, null)) return false;
            if (Object.ReferenceEquals(this, b)) return true;

            return X == b.X && Y == b.Y;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
