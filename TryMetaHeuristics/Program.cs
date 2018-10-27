using System;
using System.IO;
using TryMetaHeuristics.Algorithms;
using TryMetaHeuristics.DataStructure;

namespace TryMetaHeuristics
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph;

            {
                Console.WriteLine("Loading Graph...");
                StreamReader reader = new StreamReader("sample-graph.txt");
                string line = reader.ReadLine();
                graph = new Graph(int.Parse(line));

                string[] names = reader.ReadLine().Split(' ');
                int i = 0;
                foreach (string name in names)
                {
                    if (name != "")
                    {
                        graph.NameVertex(i, name);
                    }
                    i++;
                }

                while (!reader.EndOfStream)
                {
                    string[] row = reader.ReadLine().Split(' ');
                    string v1 = row[0];
                    string v2 = row[1];
                    int weight = int.Parse(row[2]);
                    graph.AddEdge(v1, v2, weight);
                }

                Console.WriteLine(graph);
            }

            SingleState.Run(graph);

            Console.WriteLine("==========");

            Console.Read();
        }
    }
}
