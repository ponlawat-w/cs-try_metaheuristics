using System;
using System.Collections.Generic;
using System.Text;
using TryMetaHeuristics.DataStructure;

namespace TryMetaHeuristics.BasicDiscretePopulationAlgorithm
{
    public static class BasicDiscretePopulation
    {
        public static void Run(
            Graph graph,
            int size = 100,
            int iteration = 500,
            int killBirthSize = 5)
        {
            Console.WriteLine($"Basic Discrete-Based Population ({size} Individuals)");

            Population popl = new Population(graph, size);
            Individual best = null;
            foreach (Individual indiv in popl)
            {
                if (best == null || graph.Evaluate(indiv) < graph.Evaluate(best))
                {
                    best = indiv;
                }
            }
            best = best.Clone();
            Console.Write("#0\t");
            graph.Evaluate(best, true);

            double mutateProb = 0.7;
            double mutateChangeRate = mutateProb / (iteration * 1.2);
            for (int i = 0; i < iteration; i++)
            {
                Console.Write($"{i + 1}/{iteration} (MutationProb: {mutateProb})\r");
                foreach (Individual indiv in popl)
                {
                    if (Global.InProbability(mutateProb))
                    {
                        indiv.Mutate(mutateProb);
                        if (graph.Evaluate(indiv) < graph.Evaluate(best))
                        {
                            best = indiv;
                            Console.Write($"#{i + 1}\t");
                            graph.Evaluate(best, true);
                        }
                    }
                }
                best = best.Clone();

                for (int c = 0; c < killBirthSize; c++)
                {
                    Individual badIndividual = popl.TournamentSelect(graph, 50, false);
                    popl.Kill(badIndividual);

                    Individual newIndividual = best.Clone();
                    newIndividual.Mutate(mutateProb);
                    popl.Add(newIndividual);
                    if (graph.Evaluate(newIndividual) < graph.Evaluate(best))
                    {
                        best = newIndividual;
                        Console.Write($"!{i + 1}\t");
                        graph.Evaluate(best, true);
                    }
                }
                best = best.Clone();

                mutateProb -= mutateChangeRate;
            }

            Console.WriteLine("\nOK");
        }
    }
}
