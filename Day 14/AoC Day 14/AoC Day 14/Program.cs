using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC_Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 14 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var polymerization = ParseInput(input);

            Part1(polymerization);
            Part2(polymerization);
        }

        public static Tuple<string, Dictionary<string, char>> ParseInput(string[] input)
        {
            var compound = "";
            var instructions = new Dictionary<string, char>();

            var instrRegex = new Regex(@"([A-Z]+) -> ([A-Z])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            for (var i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    compound = input[i];
                    continue;
                }                

                if (instrRegex.IsMatch(input[i]))
                {
                    var matches = instrRegex.Match(input[i]);
                    instructions.Add(matches.Groups[1].Value, matches.Groups[2].Value.First());
                }
            }

            return new Tuple<string, Dictionary<string, char>>(compound, instructions);
        }

        public static string Polymerize(string compound, Dictionary<string, char> instructions)
        {
            var newCompound = new StringBuilder(compound.Length);
            
            newCompound.Append(compound.First());

            for (int i = 0, j = 1; j < compound.Length; i++, j++)
            {
                var subCompound = compound.Substring(i, 2);

                if (instructions.ContainsKey(subCompound))
                {
                    newCompound.Append(instructions[subCompound]);
                    newCompound.Append(compound[j]);
                }
                else
                    newCompound.Append(subCompound);
            }
            return newCompound.ToString();
        }

        public static string SimplePolymerization(string compound, Dictionary<string, char> instructions, uint times)
        {
            var finalCompound = compound;

            for (var i = 0; i < times; i++)
                finalCompound = Polymerize(finalCompound, instructions);

            return finalCompound;
        }

        public static Dictionary<char, ulong> SimulateJumboPolymerization(string compound, Dictionary<string, char> instructions, uint times)
        {
            var elementFrequency = compound.CharacterFrequencyMap();

            //Break compound in to elemental pairs
            var couplets = new Dictionary<string, ulong> ();
            for (var i = 1; i < compound.Length; i++)
            {
                var couplet = compound.Substring(i - 1, 2);
                couplets.AddOrIncrement(couplet);
            }
            //Polymerize n times
            for (var i = 0; i < times; i++)
            {
                //Polymerize Everything
                var tmp = new Dictionary<string, ulong>();
                foreach (var kvp in couplets)
                {
                    char? insertion = null;
                    if (instructions.ContainsKey(kvp.Key))
                        insertion = instructions[kvp.Key];

                    if (insertion == null)
                    {
                        tmp.AddOrIncrement(kvp.Key);
                    }
                    else
                    {
                        var newPairA = new string(new char[] { kvp.Key.First(), insertion.Value });
                        var newPairB = new string(new char[] { insertion.Value, kvp.Key.Last() });

                        tmp.AddOrIncrement(newPairA, kvp.Value);
                        tmp.AddOrIncrement(newPairB, kvp.Value);

                        elementFrequency.AddOrIncrement(insertion.Value, kvp.Value);
                    }
                }
                couplets = tmp;
            }

            return elementFrequency;
        }

        public static void Part1(Tuple<string, Dictionary<string, char>> input)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var compound = SimplePolymerization(input.Item1, input.Item2, 10u);

            var elementFrequency = compound.CharacterFrequencyMap();
            var mostOccurances = elementFrequency.Max(kvp => kvp.Value);
            var leastOccurances = elementFrequency.Min(kvp => kvp.Value);

            Console.WriteLine($"Difference Between occurances of most and least common elements: {mostOccurances - leastOccurances}");
            Console.WriteLine();
        }

        public static void Part2(Tuple<string, Dictionary<string, char>> input)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var elementFrequency = SimulateJumboPolymerization(input.Item1, input.Item2, 40u);
            var mostOccurances = elementFrequency.Max(kvp => kvp.Value);
            var leastOccurances = elementFrequency.Min(kvp => kvp.Value);

            Console.WriteLine($"Difference Between occurances of most and least common elements: {mostOccurances - leastOccurances}");
            Console.WriteLine();
        }
    }
}
