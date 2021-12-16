using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_12
{
    public static class Extensions
    {
        public static void DFS(this UndirectedGraph g, Vertex u, Vertex d, HashSet<Vertex> visited, List<Vertex> pathList, List<List<Vertex>> completedPaths)
        {
            if (u.Equals(d))
            {
                Console.WriteLine(u.Name + ", " + String.Join(", ", pathList.Select(v => v.Name)));
                completedPaths.Add(pathList);
                return;
            }

            if (!u.Name.ToUpper().Equals(u.Name, StringComparison.Ordinal))
                visited.Add(u);

            foreach (var v in g.AdjacencyList[u])
            {
                if (!visited.Contains(v))
                {
                    pathList.Add(v);
                    g.DFS(v, d, visited, pathList, completedPaths);

                    pathList.Remove(v);
                }
            }

            visited.Remove(u);
        }
    }
}
