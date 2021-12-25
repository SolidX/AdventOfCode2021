using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_Day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+==========================+");
            Console.WriteLine("| Advent of Code -- Day 12 |");
            Console.WriteLine("+==========================+");

            var input = File.ReadAllLines("./input");
            var caveNetwork = ParseInput(input);

            Part1(caveNetwork);
            Part2(caveNetwork);
        }

        public static UndirectedGraph ParseInput(string[] input)
        {
            var graph = new UndirectedGraph();

            foreach (var line in input)
            {
                var x = line.Split('-');
                Vertex a, b;

                if (!graph.Contains(x[0]))
                {
                    a = new Vertex(x[0]) { MultiVisit = x[0].ToUpper().Equals(x[0], StringComparison.Ordinal) };
                    graph.AddVertex(a);
                }
                else
                    a = graph.GetVertexByName(x[0]);

                if (!graph.Contains(x[1]))
                {
                    b = new Vertex(x[1]) { MultiVisit = x[1].ToUpper().Equals(x[1], StringComparison.Ordinal) };
                    graph.AddVertex(b);
                }
                else
                    b = graph.GetVertexByName(x[1]);

                graph.AddEdge(a, b);
            }

            return graph;
        }

        public static void Part1(UndirectedGraph caveNetwork)
        {
            Console.WriteLine("~ Part 1 ~");
            Console.WriteLine();

            var start = caveNetwork.GetVertexByName("start");
            var end  = caveNetwork.GetVertexByName("end");
            var s = new Stack<Vertex>();
            s.Push(start);
            var routes = new List<string>();
            caveNetwork.DFS(start, end, new HashSet<Vertex>(), s, routes);

            Console.WriteLine($"There are {routes.Count} routes available.");
            Console.WriteLine();
        }

        public static void Part2(UndirectedGraph caveNetwork)
        {
            Console.WriteLine("~ Part 2 ~");
            Console.WriteLine();

            var start = caveNetwork.GetVertexByName("start");
            var end = caveNetwork.GetVertexByName("end");
            var s = new Stack<Vertex>();
            s.Push(start);
            var routes = new List<string>();
            caveNetwork.DFSSingleDoubleVisit(start, end, s, routes);

            Console.WriteLine($"There are {routes.Count} routes available.");
            Console.WriteLine();
        }
    }
}
