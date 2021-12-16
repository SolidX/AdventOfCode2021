using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_Day_15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 15 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var riskMap = ParseInput(input);
            
            Part1(riskMap);
            Part2(riskMap);
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

        public static ushort[][] OffsetValues(ushort[][] source, ushort offset)
        {
            var target = new ushort[source.Length][];
            for (var i = 0; i < source.Length; i++)
            {
                target[i] = new ushort[source[i].Length];

                for (var j = 0; j < source[i].Length; j++)
                {
                    var newValue = (ushort)(source[i][j] + offset);
                    if (newValue > 9)
                        newValue -= 9;

                    target[i][j] = newValue;
                }
            }

            return target;
        }

        public static ushort[][] TileInput(ushort[][] source, ushort tiles)
        {
            var offsets = new Dictionary<ushort, ushort[][]>() { { 0, source } };

            for (UInt16 i = 1; i <= 2 * (tiles - 1); i++)
                offsets.Add(i, OffsetValues(source, i));
            
            var rows = new List<ushort[]>(source.Length * tiles);
            for (UInt16 q = 0; q < tiles; q++)
            {
                var cols = new List<ushort>();
                
                for (var y = 0; y < source.Length; y++)
                {
                    for (UInt16 p = (ushort)(0 + q); p < tiles + q; p++)
                    {
                        cols.AddRange(offsets[p][y]);
                    }
                    
                    rows.Add(cols.ToArray());
                    cols.Clear();
                }
            }

            return rows.ToArray();
        }

        public static void Part1(ushort[][] riskMap)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var startPt = new Coordinate(0, 0);
            var endPt = new Coordinate(riskMap[riskMap.Length - 1].Length - 1, riskMap.Length - 1);

            var path = riskMap.A_Star(startPt, endPt);
            
            var riskLevel = 0u;
            while (path.Count > 0)
            {
                var location = path.Pop();

                if (!location.Equals(startPt))
                    riskLevel += riskMap[location.Y][location.X];
            }

            Console.WriteLine($"Path with lowest Risk Level: {riskLevel}");
            Console.WriteLine();
        }

        public static void Part2(ushort[][] riskMap)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var fullMap = TileInput(riskMap, 5);
            var startPt = new Coordinate(0, 0);
            var endPt = new Coordinate(fullMap[fullMap.Length - 1].Length - 1, fullMap.Length - 1);

            var path = fullMap.A_Star(startPt, endPt);

            var riskLevel = 0u;
            while (path.Count > 0)
            {
                var location = path.Pop();

                if (!location.Equals(startPt))
                    riskLevel += fullMap[location.Y][location.X];
            }

            Console.WriteLine($"Path with lowest Risk Level: {riskLevel}");
            Console.WriteLine();
        }
    }
}
