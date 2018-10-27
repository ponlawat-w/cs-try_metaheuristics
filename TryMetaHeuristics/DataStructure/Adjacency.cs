using System;
using System.Collections.Generic;
using System.Linq;

namespace TryMetaHeuristics.DataStructure
{
    public class Adjacency
    {
        public List<Edge> Edges { get; } = new List<Edge>();

        public Edge GetEdge(int vertex)
        {
            return this.Edges.Where(e => e.Connects(vertex)).FirstOrDefault();
        }

        public bool Connects(int vertex)
        {
            return this.GetEdge(vertex) != null;
        }

        public void AddEdge(int vertex, int weight)
        {
            if (!this.Connects(vertex))
            {
                this.Edges.Add(new Edge(vertex, weight));
            }
        }
    }
}
