using System;
using System.IO;

namespace AoC_Day_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 01 |");
            Console.WriteLine("+==========================+");

            var depthReadings = File.ReadAllLines("./input");

            Part1(depthReadings);
            Part2(depthReadings);
        }

        public static void Part1(string[] depthReadings)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            int? prevReading = null;
            int? currReading = null;

            var depthIncreases = 0;

            foreach (var reading in depthReadings)
            {
                prevReading = currReading;
                currReading = Int32.Parse(reading);

                if (prevReading < currReading)
                    depthIncreases++;
            }

            Console.WriteLine($"Depth Increased {depthIncreases} time(s).");
            Console.WriteLine();
        }

        public static void Part2(string[] depthReadings)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            int? prevWindow = null;
            int? currWindow = null;

            var depthIncreases = 0;

            for (int a = 0, b = 1, c = 2; c < depthReadings.Length; a++, b++, c++)
            {
                prevWindow = currWindow;
                currWindow = Int32.Parse(depthReadings[a]) + Int32.Parse(depthReadings[b]) + Int32.Parse(depthReadings[c]);

                if (prevWindow < currWindow)
                    depthIncreases++;
            }

            Console.WriteLine($"Depth Increased {depthIncreases} time(s).");
            Console.WriteLine();
        }
    }
}
