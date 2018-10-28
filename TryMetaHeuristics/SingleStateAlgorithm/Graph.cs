using System;
using System.Collections.Generic;
using System.Linq;
using TryMetaHeuristics.SingleStateAlgorithm;

namespace TryMetaHeuristics.DataStructure
{
    public partial class Graph
    {
        public Individual GenerateIndividual()
        {
            return new Individual(this.Vertices);
        }

        public int Evaluate(Individual individual, bool print = false)
        {
            IEnumerable<Visiting> sortedVisitings = individual.Visitings.OrderBy(v => v.Weight);
            int total = 0;
            Visiting previousVisit = null;
            foreach (Visiting currentVisit in sortedVisitings)
            {
                if (previousVisit != null)
                {
                    int v1 = previousVisit.Vertex;
                    int v2 = currentVisit.Vertex;
                    total += this.GetEdge(v1, v2).Weight;
                }
                previousVisit = currentVisit;
            }

            if (print)
            {
                IEnumerable<string> sortedNames = sortedVisitings
                    .Select(visiting => this.NameOf(visiting.Vertex));
                Console.WriteLine(String.Join(" => ", sortedNames) + $": {total}");
            }

            return total;
        }
    }
}
