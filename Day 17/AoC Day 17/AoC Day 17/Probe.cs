using System;
using System.Collections.Generic;

namespace AoC_Day_17
{
    public class Probe
    {
        public Coordinate Position { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public Probe()
        {
            Position = new Coordinate(0, 0);
            XVelocity = 0;
            YVelocity = 0;
        }

        public void Move()
        {
            Position = NextPos();

            if (XVelocity != 0)
            {
                if (XVelocity > 0)
                    XVelocity--;
                else
                    XVelocity++;
            }

            YVelocity--; //It's a long way down, but at least we can go fast
        }

        public Coordinate NextPos()
        {
            return new Coordinate(Position.X + XVelocity, Position.Y + YVelocity);
        }

        public bool HasOvershot(Rectangle targetArea)
        {
            return Math.Max(targetArea.A.X, targetArea.B.X) < Position.X || Position.Y < Math.Min(targetArea.A.Y, targetArea.B.Y);
        }

        public List<Coordinate> ProjectTrajectory(Rectangle targetArea)
        {
            var initialPos = new Coordinate(Position.X, Position.Y);
            var initialXV = XVelocity;
            var initialYV = YVelocity;

            var points = new List<Coordinate>();

            while (!HasOvershot(targetArea))
            {
                Move();
                points.Add(new Coordinate(Position.X, Position.Y));
            }

            Position = initialPos;
            XVelocity = initialXV;
            YVelocity = initialYV;

            return points;
        }
    }
}
