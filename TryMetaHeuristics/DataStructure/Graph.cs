using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TryMetaHeuristics.DataStructure
{
    public class Graph
    {
        public Adjacency[] Adjacencies;
        public int Vertices { get; }
        public IDictionary<int, string> NamesMapping;
        public IDictionary<string, int> BackwardNamesMapping;

        public Graph(int vertices)
        {
            this.Vertices = vertices;
            this.Adjacencies = new Adjacency[vertices];
            this.NamesMapping = new Dictionary<int, string>();
            this.BackwardNamesMapping = new Dictionary<string, int>();
            for (int i = 0; i < this.Vertices; i++)
            {
                this.Adjacencies[i] = new Adjacency();
                this.NameVertex(i, i.ToString());
            }
        }

        public void NameVertex(int vertex, string name)
        {
            this.NamesMapping[vertex] = name;
            this.BackwardNamesMapping[name] = vertex;
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            this.Adjacencies[vertex1].AddEdge(vertex2, weight);
        }

        public void AddEdge(string vertex1Name, string vertex2Name, int weight)
        {
            this.AddEdge(this.ValueOf(vertex1Name), this.ValueOf(vertex2Name), weight);
        }

        public Edge GetEdge(int vertex1, int vertex2)
        {
            return this.Adjacencies[vertex1].GetEdge(vertex2);
        }

        public Edge GetEdge(string vertex1name, string vertex2Name)
        {
            return this.GetEdge(this.ValueOf(vertex1name), this.ValueOf(vertex2Name));
        }

        public string NameOf(int vertex)
        {
            return this.NamesMapping[vertex];
        }

        public int ValueOf(string name)
        {
            return this.BackwardNamesMapping[name];
        }

        public int?[,] GetAdjacencyMatrix()
        {
            int?[,] matrix = new int?[this.Vertices, this.Vertices];
            for (int i = 0; i < this.Vertices; i++)
            {
                for (int j = 0; j < this.Vertices; j++)
                {
                    Edge edge = this.GetEdge(i, j);
                    matrix[i, j] = edge == null ? (int?)null : edge.Weight;
                }
            }

            return matrix;
        }

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

        public override string ToString()
        {
            string str = $"Graph Size: {this.Vertices}\n\t";
            str += String.Join('\t', this.NamesMapping.Select(m => m.Value));
            str += "\n";
            for (int i = 0; i < this.Vertices; i++)
            {
                str += this.NameOf(i);
                for (int j = 0; j < this.Vertices; j++)
                {
                    Edge edge = this.GetEdge(i, j);
                    str += '\t';
                    str += edge == null ? "N" : edge.Weight.ToString();
                }
                str += '\n';
            }

            return str;
        }
    }
}
