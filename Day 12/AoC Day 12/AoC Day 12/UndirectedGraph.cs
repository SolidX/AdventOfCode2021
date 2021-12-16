using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_12
{
    public class UndirectedGraph
    {
        private readonly NodeEqualityComparer _vertexComparer;
        public Dictionary<string, Vertex> Vertices { get; set; }

        public Dictionary<Vertex, HashSet<Vertex>> AdjacencyList { get; set; }

        public int VertexCount => Vertices.Count;

        public UndirectedGraph()
        {
            _vertexComparer = new NodeEqualityComparer();
            Vertices = new Dictionary<string, Vertex>();
            AdjacencyList = new Dictionary<Vertex, HashSet<Vertex>>(_vertexComparer);
        }

        public bool Contains(string vName)
        {
            return Vertices.ContainsKey(vName);
        }

        public bool Contains(Vertex v)
        {
            return Vertices.ContainsKey(v.Name);
        }

        public void AddVertex(Vertex v)
        {
            Vertices.Add(v.Name, v);
            AdjacencyList.Add(v, new HashSet<Vertex>(_vertexComparer));
        }

        public Vertex GetVertexByName(string n)
        {
            return Vertices.ContainsKey(n) ? Vertices[n] : null;
        }

        public void RemoveVertex(Vertex v)
        {   
            var entries = AdjacencyList[v];
            foreach (var u in entries)
                AdjacencyList[u].Remove(v);
            AdjacencyList.Remove(v);

            Vertices.Remove(v.Name);
        }

        public void AddEdge(Vertex u, Vertex v)
        {
            AdjacencyList[u].Add(v);
            AdjacencyList[v].Add(u);
        }

        public void RemoveEdge(Vertex u, Vertex v)
        {
            AdjacencyList[u].Remove(v);
            AdjacencyList[v].Remove(u);
        }
    }
}
