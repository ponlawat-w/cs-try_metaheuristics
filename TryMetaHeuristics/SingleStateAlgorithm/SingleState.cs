using System;
using TryMetaHeuristics.DataStructure;

namespace TryMetaHeuristics.SingleStateAlgorithm
{
    public static class SingleState
    {
        public static void Run(
            Graph graph,
            int iterations = 10000,
            double mean = 0.0,
            double std = 0.1)
        {
            Console.WriteLine("Single-State (One Individual)");
            Individual p = graph.GenerateIndividual();
            Console.Write("#0\t");
            double best = graph.Evaluate(p, true);
            Individual b = p.Clone();
            for (int i = 0; i < iterations; i++)
            {
                Console.Write($"{i + 1}/{iterations}\r");
                p.Tweak(mean, std);
                double newCost = graph.Evaluate(p);
                if (newCost < best)
                {
                    best = newCost;
                    b = p.Clone();
                    Console.Write($"#{i + 1}\t");
                    graph.Evaluate(p, true);
                }
            }
            Console.WriteLine("\nOK");
        }
    }
}
