using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC_Day_13
{
    public class FoldableGrid
    {
        public List<Coordinate> Points { get; set; }
        public Queue<Tuple<char, int>> FoldingInstructions { get; set; }

        public void Fold()
        {
            if (FoldingInstructions.Count > 0)
            {
                var inst = FoldingInstructions.Dequeue();
                switch (inst.Item1)
                {
                    case 'x':
                        FoldLeft(inst.Item2);
                        break;
                    case 'y':
                        FoldUp(inst.Item2);
                        break;
                }
            }
        }

        public void FoldUp(int y)
        {
            foreach (var pt in Points)
            {
                if (y >= pt.Y)
                    continue;

                pt.Y = y - (pt.Y - y);
            }
            Points = Points.Distinct().ToList();
        }
        public void FoldLeft(int x)
        {
            foreach (var pt in Points)
            {
                if (x >= pt.X)
                    continue;

                pt.X = x - (pt.X - x);
            }
            Points = Points.Distinct().ToList();
        }

        public override string ToString()
        {
            var minX = Points.Min(pt => pt.X);
            var minY = Points.Min(pt => pt.Y);
            var maxX = Points.Max(pt => pt.X) + 1;
            var maxY = Points.Max(pt => pt.Y) + 1;

            var output = new char[maxY - minY][];

            for (var i = 0; i < output.Length; i++)
            {
                var tmp = new char[maxX - minX];
                Array.Fill(tmp, ' ');
                output[i] = tmp;
            }

            foreach (var pt in Points)
                output[pt.Y - minY][pt.X - minX] = '\u25A0'; 


            var bld = new StringBuilder();
            for (var i = 0; i < output.Length; i++)
            {
                bld.Append(new string(output[i]));
                bld.Append(Environment.NewLine);
            }

            return bld.ToString();
        }
    }
}
