using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_Day_18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 18 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var numbers = InputParser.Parse(input);

            Part1(numbers);
            Part2(numbers);
        }

        public static void Part1(List<SnailfishNumber> numbers)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var sum = numbers.Sum();

            Console.WriteLine($"The sum is {sum}");
            Console.WriteLine($"The magnitude of the sum is {sum.Magnitude()}");
        }

        public static void Part2(List<SnailfishNumber> numbers)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var biggestMagnitude = 0;
            SnailfishNumber a = null;
            SnailfishNumber b = null;

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 0; j < numbers.Count; j++)
                {
                    if (i == j) continue;
                    
                    a = numbers[i];
                    b = numbers[j];
                    var sum = a + b;
                    var mag = sum.Magnitude();

                    if (mag > biggestMagnitude)
                        biggestMagnitude = mag;
                }
            }

            Console.WriteLine($"The largest magnitude of any sum of two different snailfish numbers is {biggestMagnitude}");
            Console.WriteLine();
        }
                
    }
}
