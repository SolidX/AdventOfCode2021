using System;
using System.IO;

namespace AoC_Day_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 02 |");
            Console.WriteLine("+==========================+");

            var directions = File.ReadAllLines("./input");
            Part1(directions);
            Part2(directions);

        }

        public static void Part1(string[] directions)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var hPos = 0;
            var vPos = 0;

            foreach(var instruction in directions)
            {
                var i = instruction.Split(' ', StringSplitOptions.TrimEntries);
                
                if (i.Length != 2)
                    throw new ArgumentException("Navigation instruction contains unexpected number of arguments.");
                
                switch (i[0])
                {
                    case "forward":
                        hPos += Int32.Parse(i[1]);
                        break;
                    case "down":
                        vPos += Int32.Parse(i[1]);
                        break;
                    case "up":
                        vPos -= Int32.Parse(i[1]);
                        break;
                    default:
                        throw new ArgumentException("Navigation instruction contains unexpected directional command.");
                }
            }

            Console.WriteLine($"Horizontal Position: {hPos}");
            Console.WriteLine($"Vertical Position  : {vPos}");
            Console.WriteLine($"Multipled together: {vPos * hPos}");
            Console.WriteLine();
        }

        public static void Part2(string[] directions)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var hPos = 0;
            var vPos = 0;
            var aim = 0;

            foreach (var instruction in directions)
            {
                var i = instruction.Split(' ', StringSplitOptions.TrimEntries);

                if (i.Length != 2)
                    throw new ArgumentException("Navigation instruction contains unexpected number of arguments.");

                switch (i[0])
                {
                    case "forward":
                        hPos += Int32.Parse(i[1]);
                        vPos += (aim * Int32.Parse(i[1]));
                        break;
                    case "down":
                        aim += Int32.Parse(i[1]);
                        break;
                    case "up":
                        aim -= Int32.Parse(i[1]);
                        break;
                    default:
                        throw new ArgumentException("Navigation instruction contains unexpected directional command.");
                }
            }

            Console.WriteLine($"Horizontal Position: {hPos}");
            Console.WriteLine($"Vertical Position  : {vPos}");
            Console.WriteLine($"Multipled together: {vPos * hPos}");
            Console.WriteLine();
        }
    }
}
