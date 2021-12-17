using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day_16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 16 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            //var bits = ConvertToBits("D2FE28"); //Literal Value
            //var bits = ConvertToBits("38006F45291200"); //Operator - LType 0
            //var bits = ConvertToBits("EE00D40C823060"); //Operator - LType 1

            //var bits = ConvertToBits("C200B40A82");     // 1 + 2 = 3
            //var bits = ConvertToBits("04005AC33890");   // 6 * 9 = 54
            //var bits = ConvertToBits("880086C3E88112"); // Min (7, 8, 9)
            //var bits = ConvertToBits("CE00C43D881120"); // Max (7, 8, 9)
            //var bits = ConvertToBits("D8005AC2A8F0");   // 5 < 15 == 1
            //var bits = ConvertToBits("F600BC2D8F");     // 5 > 15 == 0
            //var bits = ConvertToBits("9C005AC2F8F0");   // 5 == 15 == 0
            //var bits = ConvertToBits("9C0141080250320F1802104A08"); // 1 + 3 == 2 * 2

            var bits = ConvertToBits(input[0]);
            var p = Packet.Create(bits);

            Part1(p);
            Part2(p);
        }

        public static Queue<bool> ConvertToBits(string input)
        {
            var output = new Queue<bool>();

            //Not my most graceful moment but it works
            foreach (var c in input)
            {
                switch (c)
                {
                    case '0':
                        output.AddRange(new bool[] { false, false, false, false });
                        break;
                    case '1':
                        output.AddRange(new bool[] { false, false, false, true });
                        break;
                    case '2':
                        output.AddRange(new bool[] { false, false, true, false });
                        break;
                    case '3':
                        output.AddRange(new bool[] { false, false, true, true });
                        break;
                    case '4':
                        output.AddRange(new bool[] { false, true, false, false });
                        break;
                    case '5':
                        output.AddRange(new bool[] { false, true, false, true });
                        break;
                    case '6':
                        output.AddRange(new bool[] { false, true, true, false });
                        break;
                    case '7':
                        output.AddRange(new bool[] { false, true, true, true });
                        break;
                    case '8':
                        output.AddRange(new bool[] { true, false, false, false });
                        break;
                    case '9':
                        output.AddRange(new bool[] { true, false, false, true });
                        break;
                    case 'A':
                        output.AddRange(new bool[] { true, false, true, false });
                        break;
                    case 'B':
                        output.AddRange(new bool[] { true, false, true, true });
                        break;
                    case 'C':
                        output.AddRange(new bool[] { true, true, false, false });
                        break;
                    case 'D':
                        output.AddRange(new bool[] { true, true, false, true });
                        break;
                    case 'E':
                        output.AddRange(new bool[] { true, true, true, false });
                        break;
                    case 'F':
                        output.AddRange(new bool[] { true, true, true, true });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return output;
        }

        public static void UnpackPackets(Packet p, List<Packet> visited)
        {
            visited.Add(p);

            foreach (var sp in p.Subpackets)
                UnpackPackets(sp, visited);
        }

        public static void Part1(Packet p)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var allPackets = new List<Packet>();
            UnpackPackets(p, allPackets);

            Console.WriteLine($"Sum of all packet version nubmers: {allPackets.Sum(p => p.Version)}");
            Console.WriteLine();
        }

        public static void Part2(Packet p)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var result = p.Evaluate();

            Console.WriteLine($"Packet Evaluates to: {result}");
            Console.WriteLine();
        }
    }
}
