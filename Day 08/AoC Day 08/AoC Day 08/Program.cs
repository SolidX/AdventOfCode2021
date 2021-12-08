using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_08
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 08 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var displays = ParseInput(input);

            Part1(displays);
            Part2(displays);
        }

        public static List<Tuple<IEnumerable<SevenSegmentDisplay>, IEnumerable<SevenSegmentDisplay>>> ParseInput(string[] file)
        {
            var displays = new List<Tuple<IEnumerable<SevenSegmentDisplay>, IEnumerable<SevenSegmentDisplay>>>();
            foreach (var line in file)
            {
                var tmp = line.Split('|', StringSplitOptions.TrimEntries);
                var inputs = tmp[0].ToDisplays();
                var outputs = tmp[1].ToDisplays();

                displays.Add(new Tuple<IEnumerable<SevenSegmentDisplay>, IEnumerable<SevenSegmentDisplay>>(inputs, outputs));
            }
            return displays;
        }

        public static void Part1(List<Tuple<IEnumerable<SevenSegmentDisplay>, IEnumerable<SevenSegmentDisplay>>> displays)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var allOutputs = displays.SelectMany(d => d.Item2);
            var count = allOutputs.Count(o => o.ActivatedSegmentCount() == 2 || o.ActivatedSegmentCount() == 3 || o.ActivatedSegmentCount() == 4 || o.ActivatedSegmentCount() == 7);

            Console.WriteLine($"Outputs for '1', '4', '7', '8' appear {count} times.");
            Console.WriteLine();
        }

        public static Dictionary<char, SevenSegmentDisplay> Decode(IEnumerable<SevenSegmentDisplay> setOfDigits)
        {
            var display_1 = setOfDigits.Single(d => d.ActivatedSegmentCount() == 2);
            var display_7 = setOfDigits.Single(d => d.ActivatedSegmentCount() == 3);
            var display_4 = setOfDigits.Single(d => d.ActivatedSegmentCount() == 4);
            var display_8 = setOfDigits.Single(d => d.ActivatedSegmentCount() == 7);

            var decoded = new Dictionary<char, SevenSegmentDisplay>();
            decoded.Add('1', display_1);
            decoded.Add('7', display_7);
            decoded.Add('4', display_4);
            decoded.Add('8', display_8);

            var lit_5 = setOfDigits.Where(d => d.ActivatedSegmentCount() == 5).ToList(); // 2 or 3 or 5
            var lit_6 = setOfDigits.Where(d => d.ActivatedSegmentCount() == 6).ToList(); // 0 or 6 or 9

            //Find 6
            decoded.Add('6', lit_6.Single(d => !display_1.Intersect(d).Equals(display_1)));
            lit_6.Remove(decoded['6']);

            //Find 3
            decoded.Add('3', lit_5.Single(d => display_1.Intersect(d).Equals(display_1)));
            lit_5.Remove(decoded['3']);

            //Find 0 & 9
            decoded.Add('9', lit_6.Single(d => display_4.Intersect(d).Equals(display_4)));
            lit_6.Remove(decoded['9']);
            decoded.Add('0', lit_6.Single());
            lit_6.Clear();

            //Find 2 & 5
            decoded.Add('5', lit_5.Single(d => d.Exclude(display_4).ActivatedSegmentCount() == 2));
            lit_5.Remove(decoded['5']);
            decoded.Add('2', lit_5.Single());
            lit_5.Clear();

            return decoded;
        }

        public static void Part2(List<Tuple<IEnumerable<SevenSegmentDisplay>, IEnumerable<SevenSegmentDisplay>>> displays)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var sum = 0;
            foreach (var d in displays)
            {
                var decodedInputs = Decode(d.Item1);
                var decodedOutputs = d.Item2.Select(o => decodedInputs.SingleOrDefault(x => x.Value.Equals(o))).Select(x => x.Key).ToArray();
                var num = Int32.Parse(String.Concat(decodedOutputs));
                sum += num;
            }

            Console.WriteLine($"Sum of output values: {sum}");
            Console.WriteLine();
        }
    }
}
