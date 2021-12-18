using System;

namespace AoC_Day_17
{
    public class Rectangle
    {
        public Coordinate A { get; set; }
        public Coordinate B { get; set; }

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            var a = new Coordinate(x1, y1);
            var b = new Coordinate(x2, y2);

            if (a.Equals(b)) throw new ArgumentException(); //This is a point not a rectangle

            A = a;
            B = b;
        }

        public Rectangle(Coordinate bottomLeft, Coordinate topRight)
        {
            if (bottomLeft.Equals(topRight)) throw new ArgumentException(); //This is a point not a rectangle

            A = bottomLeft;
            B = topRight;
        }

        public int Area()
        {
            return Math.Abs(B.X - A.X) * Math.Abs(B.Y - A.Y);
        }

        public bool Contains(Coordinate c)
        {
            var inXRange = false;
            var inYRange = false;

            if (A.X < B.X)
                inXRange = (A.X <= c.X) && (c.X <= B.X);
            else
                inXRange = (B.X <= c.X) && (c.X <= A.X);

            if (A.Y < B.Y)
                inYRange = (A.Y <= c.Y) && (c.Y <= B.Y);
            else
                inYRange = (B.Y <= c.Y) && (c.Y <= A.Y);

            return inXRange && inYRange;
        }
    } 
}
