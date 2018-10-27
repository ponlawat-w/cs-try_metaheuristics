using System;
using System.Collections.Generic;
using System.Text;
using TryMetaHeuristics.DataStructure;

namespace TryMetaHeuristics.Algorithms
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
            double best = graph.Evaluate(p, true);
            Individual b = p.Clone();
            for (int i = 0; i < iterations; i++)
            {
                p.Tweak(mean, std);
                double newCost = graph.Evaluate(p);
                if (newCost < best)
                {
                    best = newCost;
                    b = p.Clone();
                    Console.Write($"#{i}\t");
                    graph.Evaluate(p, true);
                }
            }
            Console.WriteLine("OK");
        }
    }
}
