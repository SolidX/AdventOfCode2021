using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_12
{
    public static class Extensions
    {
        public static void DFS(this UndirectedGraph g, Vertex u, Vertex d, HashSet<Vertex> visited, Stack<Vertex> pathList, List<string> completedPaths)
        {
            if (u.Equals(d))
            {
                var path = String.Join(", ", pathList.Select(v => v.Name).Reverse());
                completedPaths.Add(path);
                return;
            }

            if (!u.MultiVisit)
                visited.Add(u);

            foreach (var v in g.AdjacencyList[u])
            {
                if (!visited.Contains(v))
                {
                    pathList.Push(v);
                    g.DFS(v, d, visited, pathList, completedPaths);

                    pathList.Pop();
                }
            }

            visited.Remove(u);
        }

        public static void DFSSingleDoubleVisit(this UndirectedGraph g, Vertex u, Vertex d, Stack<Vertex> pathList, List<string> completedPaths)
        {
            u.Visited = pathList.Count(x => x.Name == u.Name);

            if (u.Equals(d))
            {
                var path = String.Join(", ", pathList.Select(v => v.Name).Reverse());
                completedPaths.Add(path);
                return;
            }

            var terminals = new HashSet<string>() { "start", "end" };
            var doubleVisit = g.Vertices.Values.SingleOrDefault(x => x.Visited >= 2 && !x.MultiVisit);


            foreach (var v in g.AdjacencyList[u])
            {
                // Specifically, big caves can be visited any number of times
                // a single small cave can be visited at most twice
                // and the remaining small caves can be visited at most once.
                // However, the caves named start and end can only be visited exactly once each
                var allowableVisits = doubleVisit == null ? 2 : 1;
                var terminalExclusion = terminals.Contains(v.Name) && v.Visited != 0;

                if (v.MultiVisit || (v.Visited < allowableVisits && !terminalExclusion))
                {
                    pathList.Push(v);
                    g.DFSSingleDoubleVisit(v, d, pathList, completedPaths);

                    pathList.Pop();
                    v.Visited = pathList.Count(x => x.Name == v.Name);
                }
            }

            u.Visited = pathList.Count(x => x.Name == u.Name);
        }
    }
}
