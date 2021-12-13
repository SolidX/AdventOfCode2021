using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_Day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 13 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");

            Part1(ParseInput(input));
            Part2(ParseInput(input));
        }

        public static FoldableGrid ParseInput(string[] input)
        {
            var coordinateRegex = new Regex(@"([0-9]+),([0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var foldRegex = new Regex(@"fold along (x|y)=([0-9]+)", RegexOptions.Compiled);

            var coords = new List<Coordinate>();
            var instructions = new Queue<Tuple<char, int>>();

            foreach (var line in input)
            {
                if (coordinateRegex.IsMatch(line))
                {
                    var matches = coordinateRegex.Match(line);
                    var c = new Coordinate(Int32.Parse(matches.Groups[1].Value), Int32.Parse(matches.Groups[2].Value));
                    coords.Add(c);
                    continue;
                }
                if (foldRegex.IsMatch(line))
                {
                    var matches = foldRegex.Match(line);
                    var i = new Tuple<char, int>(matches.Groups[1].Value.First(), Int32.Parse(matches.Groups[2].Value));
                    instructions.Enqueue(i);
                    continue;
                }
            }

            return new FoldableGrid { FoldingInstructions = instructions, Points = coords };
        }

        public static void Part1(FoldableGrid g)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            g.Fold();

            Console.WriteLine($"Dots Visible After 1 Fold: {g.Points.Count()}");
            Console.WriteLine();
        }

        public static void Part2(FoldableGrid g)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();
            
            var output = new List<string>();
            while (g.FoldingInstructions.Count() > 0)
            {
                g.Fold();
                output.Add(g.ToString());
            }

            using (var f = new StreamWriter("output.txt"))
            {
                f.WriteLine(output.Last());
            }

            Console.WriteLine("Activation Key:");
            Console.WriteLine(output.Last());
            Console.WriteLine();
        }
    }
}
