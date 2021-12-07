using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 06 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var school = ParseInput(input);

            Part1(school);
            Part2(school);
        }

        public static IEnumerable<Lanternfish> ParseInput(string[] input)
        {
            return input.First().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => new Lanternfish(UInt32.Parse(n)));
        }

        public static void Part1(IEnumerable<Lanternfish> school)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();
            Console.WriteLine($"Lanternfish Starting Population: {school.Count()}");

            var population = school.ToList();
            var newHatchlings = new List<Lanternfish>();

            var simulationLength = 80;
            for (var i = 0; i < simulationLength; i++)
            {
                foreach (var fish in population)
                {
                    if (fish.spawnTimer == 0)
                        newHatchlings.Add(new Lanternfish());

                    fish.timerTick();
                }
                population.AddRange(newHatchlings);
                newHatchlings.Clear();
            }

            Console.WriteLine($"Lanternfish Ending Population: {population.Count()}");
            Console.WriteLine();
        }

        public static void Part2(IEnumerable<Lanternfish> school)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();
            Console.WriteLine($"Lanternfish Starting Population: {school.Count()}");

            var populationModel = new Dictionary<uint, long>();
            foreach (var fish in school)
            {
                if (populationModel.ContainsKey(fish.spawnTimer))
                    populationModel[fish.spawnTimer]++;
                else
                    populationModel.Add(fish.spawnTimer, 1L);
            }

            var simulationLength = 256;
            for (var i = 0; i < simulationLength; i++)
            {
                var newHatchlings = 0L;
                var nextGen = new Dictionary<uint, long>();

                foreach (var group in populationModel)
                {
                    if (group.Key == 0)
                    {
                        newHatchlings = group.Value;
                        if (nextGen.ContainsKey(6))
                            nextGen[6] += group.Value;
                        else
                            nextGen[6] = group.Value;
                    }
                    else
                    {
                        if (nextGen.ContainsKey(group.Key - 1))
                            nextGen[group.Key - 1] += group.Value;
                        else
                            nextGen.Add(group.Key - 1, group.Value);
                    }
                }

                nextGen.Add(8, newHatchlings);
                populationModel = nextGen;
            }

            Console.WriteLine($"Lanternfish Ending Population: {populationModel.Values.Sum()}");
            Console.WriteLine();
        }
    }
}
