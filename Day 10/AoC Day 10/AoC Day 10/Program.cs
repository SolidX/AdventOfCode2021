using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 10 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");

            Part1(input);
            Part2(input);
        }

        public static int IllegalCharacterScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    return 0;
            }
        }

        public static ulong AutocompleteScore(Dictionary<char, char> symbolMap, Stack<char> s)
        {
            var score = 0uL;
            while (s.Count > 0)
            {
                score *= 5uL;

                var c = s.Pop();
                var completion = symbolMap.Single(kvp => kvp.Value == c).Key;                
                switch (completion)
                {
                    case ')':
                        score += 1uL;
                        break;
                    case ']':
                        score += 2uL;
                        break;
                    case '}':
                        score += 3uL;
                        break;
                    case '>':
                        score += 4uL;
                        break;
                }
            }

            return score;
        }

        public static void Part1(string[] input)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var symbolMap = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };
            var openingSymbols = symbolMap.Values.ToHashSet();
            var closingSymbols = symbolMap.Keys.ToHashSet();

            var errorScore = 0;
            foreach (var line in input)
            {
                //Evaluate validity
                var s = new Stack<char>();
                foreach (var c in line)
                {
                    if (openingSymbols.Contains(c))
                    {
                        s.Push(c);
                        continue;
                    }
                    if (closingSymbols.Contains(c))
                    {
                        if (s.Peek() == symbolMap[c])
                            s.Pop();
                        else
                        {
                            errorScore += IllegalCharacterScore(c); //Illegal Character found
                            break;
                        }
                    }
                }
                //Ignore valid or incomplete lines
            }

            Console.WriteLine($"Sum of Error Scores: {errorScore}");
            Console.WriteLine();
        }

        public static void Part2(string[] input)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var symbolMap = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };
            var openingSymbols = symbolMap.Values.ToHashSet();
            var closingSymbols = symbolMap.Keys.ToHashSet();

            var completionScores = new List<ulong>();

            foreach (var line in input)
            {
                //Evaluate validity
                var s = new Stack<char>();
                var invalid = false;
                
                foreach (var c in line)
                {
                    if (openingSymbols.Contains(c))
                    {
                        s.Push(c);
                        continue;
                    }
                    if (closingSymbols.Contains(c))
                    {
                        if (s.Peek() == symbolMap[c])
                            s.Pop();
                        else
                        {
                            //Ignore lines with invalid characters
                            invalid = true;
                            break;
                        }
                    }
                }

                //Ignore valid lines
                //Autocomplete incomplete lines
                if (!invalid && s.Count != 0)
                    completionScores.Add(AutocompleteScore(symbolMap, s));
            }

            Console.WriteLine($"Median of Autocompletion Scores: {completionScores.Median()}");
            Console.WriteLine();
        }
    }
}
