using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_09
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 09 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var depthMap = ParseInput(input);

            Part1(depthMap);
            Part2(depthMap);
        }

        public static ushort[][] ParseInput(string[] input)
        {
            var y = new List<ushort[]>(input.Length);
            foreach (var i in input)
            {
                var x = new List<ushort>(i.Length);
                foreach (var c in i)
                    x.Add(UInt16.Parse(new char[] { c }));

                y.Add(x.ToArray());
            }
            return y.ToArray();
        }

        public static List<Coordinate> GetAdjacentPoints(ushort[][] depthMap, Coordinate pt)
        {
            var adjacentPoints = new List<Coordinate>();

            if (pt.Y - 1 >= 0) adjacentPoints.Add(new Coordinate(pt.X, pt.Y - 1));
            if (pt.X - 1 >= 0) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y));
            if (pt.X + 1 < depthMap[pt.Y].Length) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y));
            if (pt.Y + 1 < depthMap.Length) adjacentPoints.Add(new Coordinate(pt.X, pt.Y + 1));

            return adjacentPoints;
        }

        public static Coordinate[] GetLowPoints(ushort[][] depthMap)
        {
            var lowPoints = new List<Coordinate>();

            for (var y = 0; y < depthMap.Length; y++)
            {
                for (var x = 0; x < depthMap[y].Length; x++)
                {
                    var currPoint = depthMap[y][x];
                    var adjacentPoints = new List<ushort>();

                    //Find adjacent points
                    if (y - 1 >= 0) adjacentPoints.Add(depthMap[y - 1][x]);
                    if (x - 1 >= 0) adjacentPoints.Add(depthMap[y][x - 1]);
                    if (x + 1 < depthMap[y].Length) adjacentPoints.Add(depthMap[y][x + 1]);
                    if (y + 1 < depthMap.Length) adjacentPoints.Add(depthMap[y + 1][x]);

                    //Mark as a low point if it's lower than all of the points around it
                    if (adjacentPoints.All(pt => pt > currPoint))
                        lowPoints.Add(new Coordinate(x, y));
                }
            }

            return lowPoints.ToArray();
        }

        public static void Part1(ushort[][] depthMap)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var lowPoints = GetLowPoints(depthMap).Select(pt => depthMap[pt.Y][pt.X]);

            //Calculate Risk Level
            var riskLevels = lowPoints.Select(pt => pt + 1);

            Console.WriteLine($"Risk Level of Low Points: {riskLevels.Sum()}");
            Console.WriteLine();
        }

        public static void Part2(ushort[][] depthMap)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var lowPoints = GetLowPoints(depthMap);
            var basins = new Dictionary<Coordinate, int>();

            foreach (var pt in lowPoints)
            {
                var adjacentPoints = new List<Coordinate>() { pt };
                var prevSize = 1;

                do
                {
                    prevSize = adjacentPoints.Count();

                    var tmp = new List<Coordinate>();
                    foreach (var x in adjacentPoints)
                    {
                        var notPeaks = GetAdjacentPoints(depthMap, x).Where(q => depthMap[q.Y][q.X] < 9);
                        tmp.Add(x);
                        tmp.AddRange(notPeaks);
                    }

                    adjacentPoints = tmp.Distinct().ToList();
                }
                while (prevSize != adjacentPoints.Count());

                basins.Add(pt, adjacentPoints.Count());                 
            }

            var largestBasins = basins.OrderByDescending(kvp => kvp.Value).Take(3);
            var basinSizes = largestBasins.Select(b => b.Value);

            Console.WriteLine($"Product of 3 Largest Basin Sizes: {basinSizes.Product()}");
            Console.WriteLine();
        }
    }
}
