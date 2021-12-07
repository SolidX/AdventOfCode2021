using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_05
{
    public class CoordinatePair
    {
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        
        public IEnumerable<Coordinate> GetPoints()
        {
            var points = new List<Coordinate>();

            if (IsHorizontalLine())
            {
                var y = End.Y;
                
                if (Start.X < End.X)
                {
                    for (var x = Start.X; x <= End.X; x++)
                        points.Add(new Coordinate(x, y));
                }
                else
                {
                    for (var x = End.X; x <= Start.X; x++)
                        points.Add(new Coordinate(x, y));
                }
                return points;
            }
            if (IsVerticalLine())
            {
                var x = End.X;

                if (Start.Y < End.Y)
                {
                    for (var y = Start.Y; y <= End.Y; y++)
                        points.Add(new Coordinate(x, y));
                }
                else
                {
                    for (var y = End.Y; y <= Start.Y; y++)
                        points.Add(new Coordinate(x, y));
                }
                return points;
            }

            //Diagonal Line
            var slope = Slope();
            var dx = Start.X < End.X ? 1 : -1;
            var dy = Start.Y < End.Y ? 1 : -1;

            int mx, my;
            for (mx = Start.X, my = Start.Y; mx != End.X + dx && my != End.Y + dy; mx += dx, my += dy)
                points.Add(new Coordinate((ushort)mx, (ushort)my));
            
            return points;
        }

        public bool IsHorizontalLine() { return End.Y == Start.Y; }
        public bool IsVerticalLine() { return End.X == Start.X; }
        public decimal Slope()
        {
            if (!IsVerticalLine())
                return (decimal)(End.Y - Start.Y) / (decimal)(End.X - Start.X);
            throw new NotImplementedException();
        }
    }
}
