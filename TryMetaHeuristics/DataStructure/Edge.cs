using System;
using System.Collections.Generic;
using System.Text;

namespace TryMetaHeuristics.DataStructure
{
    public class Edge
    {
        public int Vertex;
        public int Weight = 0;

        public Edge(int vertex, int weight = 0)
        {
            this.Vertex = vertex;
            this.Weight = weight;
        }

        public bool Connects(int vertex)
        {
            return this.Vertex == vertex;
        }
    }
}
