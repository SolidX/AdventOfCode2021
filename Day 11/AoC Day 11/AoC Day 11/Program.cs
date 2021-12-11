using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 11 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var octopuses = ParseInput(input);

            Part1(octopuses);
            Part2(octopuses);
        }

        //Day 09 coming in handy
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

        public static List<Coordinate> GetAdjacentPoints(ushort[][] map, Coordinate pt)
        {
            var adjacentPoints = new List<Coordinate>();

            if (pt.Y - 1 >= 0)
            {
                adjacentPoints.Add(new Coordinate(pt.X, pt.Y - 1));
                if (pt.X - 1 >= 0) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y -1));
                if (pt.X + 1 < map[pt.Y].Length) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y -1));
            }
            if (pt.X - 1 >= 0) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y));
            if (pt.X + 1 < map[pt.Y].Length) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y));
            if (pt.Y + 1 < map.Length)
            {
                adjacentPoints.Add(new Coordinate(pt.X, pt.Y + 1));
                if (pt.X - 1 >= 0) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y + 1));
                if (pt.X + 1 < map[pt.Y].Length) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y + 1));
            }

            return adjacentPoints;
        }

        public static long Simulate(ushort[][] map, uint ticks)
        {
            var tmpMap = map.DeepClone();

            var flashes = 0L;
            for (var i = 0; i < ticks; i++)
                flashes += Tick(tmpMap);

            return flashes;
        }

        public static long Tick(ushort[][] map)
        {
            var willFlash = new HashSet<Coordinate>(new CoordinateEqualityComparer());
            var hasFlashed = new HashSet<Coordinate>(new CoordinateEqualityComparer());

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    //First, the energy level of each octopus increases by 1.
                    map[y][x]++;

                    //Then, any octopus with an energy level greater than 9 flashes
                    if (map[y][x] > 9)
                        willFlash.Add(new Coordinate(x, y));
                }
            }

            do
            {
                var tmp = new HashSet<Coordinate>(new CoordinateEqualityComparer());
                foreach (var o in willFlash)
                {
                    //Finally, any octopus that flashed during this step has its energy level set to 0
                    map[o.Y][o.X] = 0;
                    hasFlashed.Add(o);

                    var adjacent = GetAdjacentPoints(map, o).ToHashSet(new CoordinateEqualityComparer());
                    adjacent.ExceptWith(hasFlashed);

                    foreach (var pt in adjacent)
                    {
                        map[pt.Y][pt.X]++;
                        if (map[pt.Y][pt.X] > 9)
                            tmp.Add(pt);
                    }
                }
                tmp.ExceptWith(hasFlashed);
                willFlash.Clear();
                willFlash.UnionWith(tmp);
            }
            while (willFlash.Count > 0);

            return hasFlashed.Count;
        }

        public static void Part1(ushort[][] map)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var x = Simulate(map, 100u);

            Console.WriteLine($"Number of flashes in 100 steps: {x}");
            Console.WriteLine();
        }

        public static void Part2(ushort[][] map)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var size = map.Length * map[0].Length;
            var flashes = 0L;
            var tick = 0uL;
            var tmpMap = map.DeepClone();

            do
            {
                tick++;
                flashes = Tick(tmpMap);                
            }
            while (flashes != size);

            Console.WriteLine($"Number of steps until all octopuses flash: {tick}");
            Console.WriteLine();
        }
    }
}
