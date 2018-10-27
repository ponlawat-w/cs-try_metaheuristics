using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TryMetaHeuristics.DataStructure
{
    public class Visiting
    {
        public readonly int Vertex;
        public double Weight;

        private Visiting(int vertex)
        {
            this.Vertex = vertex;
        }

        public Visiting(int vertex, double[] weights)
        {
            this.Vertex = vertex;
            do
            {
                this.Weight = Global.Random.NextDouble();
            }
            while (weights.Contains(this.Weight));
        }

        public Visiting Clone()
        {
            return new Visiting(this.Vertex)
            {
                Weight = this.Weight
            };
        }
    }
}
