using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TryMetaHeuristics.BasicDiscretePopulationAlgorithm;

namespace TryMetaHeuristics.DataStructure
{
    public partial class Graph
    {
        public int Evaluate(Individual individual, bool print = false)
        {
            int totalCost = 0;
            int previousVertex = -1;
            foreach (int currentVertex in individual.VerticesOrder)
            {
                if (previousVertex > -1)
                {
                    totalCost += this.GetEdge(previousVertex, currentVertex).Weight;
                }
                previousVertex = currentVertex;
            }

            if (print)
            {
                Console.WriteLine(
                    String.Join(" => ", individual.VerticesOrder.Select(o => this.NameOf(o)))
                    + $": {totalCost}"
                );
            }

            return totalCost;
        }
    }
}
