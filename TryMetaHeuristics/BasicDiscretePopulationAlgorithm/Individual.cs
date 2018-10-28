using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TryMetaHeuristics.DataStructure;

namespace TryMetaHeuristics.BasicDiscretePopulationAlgorithm
{
    public class Individual
    {
        public int[] VerticesOrder;

        private Individual()
        {
        }

        public Individual(Graph graph)
        {
            int vertices = graph.Vertices;
            List<int> order = new List<int>();
            
            for (int i = 0; i < vertices; i++)
            {
                int nextVertex;
                do
                {
                    nextVertex = Global.Random.Next(0, vertices);
                }
                while (order.Contains(nextVertex));
                order.Add(nextVertex);
            }

            this.VerticesOrder = order.ToArray();
        }

        public void Swap(int index1, int index2)
        {
            int temp = this.VerticesOrder[index1];
            this.VerticesOrder[index1] = this.VerticesOrder[index2];
            this.VerticesOrder[index2] = temp;
        }

        public void Mutate(double probability = 0.5)
        {
            for (int i = 0; i < this.VerticesOrder.Length; i++)
            {
                if ((1.0 - Global.Random.NextDouble()) < probability)
                {
                    int j = Global.Random.Next(0, this.VerticesOrder.Length);
                    this.Swap(i, j);
                }
            }
        }

        public Individual Clone()
        {
            return new Individual
            {
                VerticesOrder = this.VerticesOrder.Select(o => o).ToArray()
            };
        }
    }
}
