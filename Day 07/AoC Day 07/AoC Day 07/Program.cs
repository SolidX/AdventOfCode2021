using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 04 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var crabPositions = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => UInt16.Parse(n)).ToList();
            //var crabPositions = "16,1,2,0,4,2,7,1,2,14".Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => UInt16.Parse(n)).ToList();

            Part1(crabPositions);
            Part2(crabPositions);
        }
        public static void Part1(IEnumerable<ushort> positions)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var minPos = positions.Min();
            var maxPos = positions.Max();
            var median = positions.Median(); //I suspect this is the ideal spot but I can't mathematically prove that...

            //...so, Brute force! (just to be safe)
            var bestFuelConsumption = Int32.MaxValue;
            var bestPos = -1;

            for (var i = minPos; i <= maxPos; i++)
            {
                var fuelConsumption = 0;
                foreach (var crab in positions)
                    fuelConsumption += Math.Abs(crab - i);

                if (fuelConsumption < bestFuelConsumption)
                {
                    bestPos = i;
                    bestFuelConsumption = fuelConsumption;
                }
            }

            Console.WriteLine($"Optimal position: {bestPos}");
            Console.WriteLine($"Required Fuel Consumption: {bestFuelConsumption}");
            Console.WriteLine();
        }

        public static void Part2(IEnumerable<ushort> positions)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var minPos = positions.Min();
            var maxPos = positions.Max();

            var bestFuelConsumption = Int64.MaxValue;
            var bestPos = -1;

            for (var i = minPos; i <= maxPos; i++)
            {
                var fuelConsumption = 0L;
                foreach (var crab in positions)
                    fuelConsumption += Math.Abs(crab - i).TriangularSum();

                if (fuelConsumption < bestFuelConsumption)
                {
                    bestPos = i;
                    bestFuelConsumption = fuelConsumption;
                }
            }

            Console.WriteLine($"Optimal position: {bestPos}");
            Console.WriteLine($"Required Fuel Consumption: {bestFuelConsumption}");
            Console.WriteLine();
        }
    }
}
