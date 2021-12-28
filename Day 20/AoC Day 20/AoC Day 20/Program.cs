using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 20 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var enhancementStr = input[0];
            var image = ParseInput(input);

            Part1(image, enhancementStr);
            Part2(image, enhancementStr);
        }

        public static List<List<char>> ParseInput(string[] input)
        {
            var y = new List<List<char>>(input.Length - 2);

            for (int i = 2; i < input.Length; i++)
                y.Add(input[i].ToList());

            return y;
        }

        public static void Part1(List<List<char>> image, string enhancementAlgo)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();


            var enhancedImage = EnhanceImage(image, enhancementAlgo, 2u, true);
            var lit = enhancedImage.Select(row => row.Count(px => px == ImageExtensions.LIGHT_PX)).Sum();

            Console.WriteLine($"Lit Pixel Count: {lit}");
            Console.WriteLine();
        }

        public static List<List<char>> EnhanceImage(List<List<char>> image, string enhancementAlgo, uint times, bool draw = false)
        {
            var enhancedImage = image;

            for (var i = 0; i < times; i++)
            {
                var defaultPx = i % 2 == 0 ? ImageExtensions.DARK_PX : enhancementAlgo[0];

                enhancedImage = enhancedImage.Pad(2, defaultPx);

                if (draw)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Padded {i + 1}");
                    foreach (var line in enhancedImage)
                        Console.WriteLine(String.Concat(line));
                }

                enhancedImage = enhancedImage.Enhance(enhancementAlgo, defaultPx);

                if (draw)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Enhanced {i + 1}");
                    foreach (var line in enhancedImage)
                        Console.WriteLine(String.Concat(line));
                }
            }

            return enhancedImage;
        }

        public static void Part2(List<List<char>> image, string enhancementAlgo)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var enhancedImage = EnhanceImage(image, enhancementAlgo, 50u);
            var lit = enhancedImage.Select(row => row.Count(px => px == ImageExtensions.LIGHT_PX)).Sum();

            Console.WriteLine($"Lit Pixel Count: {lit}");
            Console.WriteLine();
        }
    }
}