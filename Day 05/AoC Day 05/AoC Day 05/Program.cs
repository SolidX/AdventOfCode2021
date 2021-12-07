using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_Day_05
{
    class Program
    {
        static Regex VentLocationFormat = new Regex(@"(\d+)\,(\d+) -> (\d+)\,(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 05 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var ventLines = ParseInput(input);

            Part1(ventLines);
            Part2(ventLines);
        }

        public static IEnumerable<CoordinatePair> ParseInput(string[] input)
        {
            //Ex 561,579 -> 965,175
            var lines = input.Select(x => {
                var components = VentLocationFormat.Split(x);

                return new CoordinatePair() {
                    Start = new Coordinate(UInt16.Parse(components[1]), UInt16.Parse(components[2])),
                    End = new Coordinate(UInt16.Parse(components[3]), UInt16.Parse(components[4]))
                };
            });

            return lines;
        }

        public static void Part1(IEnumerable<CoordinatePair> ventLines)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var filtered = ventLines.Where(l => l.IsHorizontalLine() || l.IsVerticalLine()).ToList();

            var ventDensityMap = filtered.GetOverlappingPointsDictionary();
            var mostDangerous = ventDensityMap.Count(pt => pt.Value >= 2);

            Console.WriteLine($"Points with overlapping vents: {mostDangerous}");
            Console.WriteLine();
        }

        public static void Part2(IEnumerable<CoordinatePair> ventLines)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var ventDensityMap = ventLines.GetOverlappingPointsDictionary();
            var mostDangerous = ventDensityMap.Count(pt => pt.Value >= 2);

            Console.WriteLine($"Points with overlapping vents: {mostDangerous}");
            Console.WriteLine();
        }
    }
}
