using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 04 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");

            //Parse Input File
            var drawnNumbers = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => UInt16.Parse(n)).ToList();
            var boards = new List<BingoBoard>();

            for (var i = 2; i < input.Length; i += 6)
            {
                var tmp = new string[5];
                tmp[0] = input[i];
                tmp[1] = input[i + 1];
                tmp[2] = input[i + 2];
                tmp[3] = input[i + 3];
                tmp[4] = input[i + 4];

                boards.Add(new BingoBoard(tmp));
            }

            Part1(boards, drawnNumbers);
            Part2(boards, drawnNumbers);
        }

        public static void Part1(List<BingoBoard> boards, List<ushort> drawings)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            for (var i = 0; i < drawings.Count(); i++)
            {
                foreach (var b in boards)
                    b.Mark(drawings[i]);

                if (i >= 4)
                {
                    //Check for winner
                    var winner = boards.SingleOrDefault(b => b.IsWinner());
                    if (winner != default(BingoBoard))
                    {
                        var sumUnmarked = winner.BoardMembers.Where(s => !s.Value.Marked).Sum(x => x.Key);
                        var boardScore = drawings[i] * sumUnmarked;

                        Console.WriteLine($"Winning Board Score: {boardScore}");
                        Console.WriteLine();
                        return;
                    }
                }
            }
        }

        public static void Part2(List<BingoBoard> boards, List<ushort> drawings)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();


            var lastWinner = default(BingoBoard);
            var stillPlaying = new List<BingoBoard>();

            for (var i = 0; i < drawings.Count(); i++)
            {
                foreach (var b in boards)
                    b.Mark(drawings[i]);

                if (i >= 4)
                {
                    //Find last winner
                    stillPlaying = boards.Where(b => !b.IsWinner()).ToList();

                    if (stillPlaying.Count() == 1)
                    {
                        lastWinner = stillPlaying.First();
                        continue;
                    }

                    if (lastWinner != default(BingoBoard) && stillPlaying.Count == 0)
                    { 
                        //Score board
                        var sumUnmarked = lastWinner.BoardMembers.Where(s => !s.Value.Marked).Sum(x => x.Key);
                        var boardScore = drawings[i] * sumUnmarked;

                        Console.WriteLine($"Last Winning Board Score: {boardScore}");
                        Console.WriteLine();
                        return;
                    }
                }
            }
        }
    }
}
