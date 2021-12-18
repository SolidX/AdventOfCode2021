using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_Day_17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 17 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var targetArea = ParseInput(input[0]);
            //var targetArea = ParseInput("target area: x=20..30, y=-10..-5");

            Part1(targetArea);
            Part2(targetArea);
        }

        public static Rectangle ParseInput(string input)
        {
            var targetRegex = new Regex(@"target area: x=(-?[0-9]+)\.\.(-?[0-9]+), y=(-?[0-9]+)\.\.(-?[0-9]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (targetRegex.IsMatch(input))
            {
                var match = targetRegex.Match(input);
                var x1 = Int32.Parse(match.Groups[1].Value);
                var x2 = Int32.Parse(match.Groups[2].Value);
                var y1 = Int32.Parse(match.Groups[3].Value);
                var y2 = Int32.Parse(match.Groups[4].Value);

                return new Rectangle(x1, y1, x2, y2);
            }
            return null;
        }

        public static void Part1(Rectangle targetArea)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var probes = new List<Probe>();
            var lowestPt = Math.Min(targetArea.A.Y, targetArea.B.Y);

            int? highestY = null;
            Probe bestProbe = null;

            for (var yV = lowestPt; yV <= Math.Abs(lowestPt); yV++)
            {
                for (var xV = 0; xV <= Math.Max(targetArea.A.X, targetArea.B.X); xV++)
                {
                    var p = new Probe
                    {
                        XVelocity = xV,
                        YVelocity = yV
                    };

                    var trajectory = p.ProjectTrajectory(targetArea);
                    if (trajectory.Any(pt => targetArea.Contains(pt)))
                    {
                        var maxY = trajectory.Max(pt => pt.Y);
                        if (!highestY.HasValue || highestY.Value < maxY)
                        {
                            highestY = maxY;
                            bestProbe = p;
                        }
                    }
                }
            }

            Console.WriteLine($"Highest Y occurs at {highestY} with X velocity {bestProbe.XVelocity} and Y velocity {bestProbe.YVelocity}");
            Console.WriteLine();
        }

        public static void Part2(Rectangle targetArea)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var lowestPt = Math.Min(targetArea.A.Y, targetArea.B.Y);

            var count = 0;

            for (var yV = lowestPt; yV <= Math.Abs(lowestPt); yV++)
            {
                for (var xV = 0; xV <= Math.Max(targetArea.A.X, targetArea.B.X); xV++)
                {
                    var p = new Probe
                    {
                        XVelocity = xV,
                        YVelocity = yV
                    };

                    var trajectory = p.ProjectTrajectory(targetArea);
                    if (trajectory.Any(pt => targetArea.Contains(pt)))
                        count++;
                }
            }

            Console.WriteLine($"Possible Initial Velocities that will land probe in target area: {count}");
            Console.WriteLine();
        }
    }
}
