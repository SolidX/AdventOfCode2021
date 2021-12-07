using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace AoC_Day_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 03 |");
            Console.WriteLine("+==========================+");

            var diagnosticReport = File.ReadAllLines("./input");
            Part1(diagnosticReport);
            Part2(diagnosticReport);
        }

        public static void Part1(string[] report)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var bits = report[0].Length;
            var count = new int[bits];

            foreach (var entry in report)
            {
                for(int i = 0; i < bits; i++)
                {
                    if (entry[i] == '1')
                        count[i]++;
                }
            }

            var padding = new bool[32 - bits];
            var mask = count.Select(x => x > (report.Length / 2)).ToArray();

            var gamma = new BitArray(padding.Concat(mask).ToArray());
            var epsilon = new BitArray(padding.Concat(mask.Select(x => !x)).ToArray());

            var powerUsage = gamma.ToInt32() * epsilon.ToInt32();

            Console.WriteLine($"Power consumption: {powerUsage}");
            Console.WriteLine();
        }

        public static void Part2(string[] report)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var bits = report[0].Length;
            
            var mostCommon = "";
            var leastCommon = "";

            var count = report.Count(x => x.StartsWith('1'));

            if (count > report.Length / 2)
            {
                mostCommon += '1';
                leastCommon += '0';
            }
            else
            {
                if (count < report.Length / 2)
                {
                    mostCommon += '0';
                    leastCommon += '1';
                }
                else
                {
                    if (count == report.Length / 2)
                    {
                        mostCommon += '1';
                        leastCommon += '0';
                    }
                }
                
            }

            var searchSpace = report.Where(x => x.StartsWith(mostCommon));

            do
            {
                var c = searchSpace.Count(x => x.StartsWith(mostCommon + '1'));
                if (c >= (decimal)searchSpace.Count() / (decimal)2)
                {
                    mostCommon += '1';
                }
                else
                {
                    mostCommon += '0';
                }

                searchSpace = searchSpace.Where(x => x.StartsWith(mostCommon));
            } while (mostCommon.Length < bits && searchSpace.Count() > 1);

            var o2 = searchSpace.Single().ToBitArray().ToInt32();
            Console.WriteLine($"Oxygen Generator Rating: {mostCommon} ({o2})");

            searchSpace = report.Where(x => x.StartsWith(leastCommon));

            do
            {
                var c = searchSpace.Count(x => x.StartsWith(leastCommon + '1'));
                if (c < (decimal)searchSpace.Count() / (decimal)2)
                {
                    leastCommon += '1';
                }
                else
                {
                    leastCommon += '0';
                }

                searchSpace = searchSpace.Where(x => x.StartsWith(leastCommon));
            } while (leastCommon.Length < bits && searchSpace.Count() > 1);

            var co2 = searchSpace.Single().ToBitArray().ToInt32();
            Console.WriteLine($"CO2 Scrubber Rating: {leastCommon} ({co2})");

            Console.WriteLine($"Submarine Life Support rating: {o2 * co2}");
            Console.WriteLine();
        }
    }
}
