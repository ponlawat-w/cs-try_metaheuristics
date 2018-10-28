using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TryMetaHeuristics.DataStructure;

namespace TryMetaHeuristics.BasicDiscretePopulationAlgorithm
{
    public class Population : IEnumerable<Individual>
    {
        List<Individual> Individuals;

        private Population() { }
        public Population(Graph graph, int size = 200)
        {
            this.Individuals = new List<Individual>();
            for (int i = 0; i < size; i++)
            {
                this.Individuals.Add(new Individual(graph));
            }
        }

        public Population Clone()
        {
            return new Population
            {
                Individuals = this.Individuals.Select(i => i).ToList()
            };
        }

        public Individual RandomSelect()
        {
            return this.Individuals[Global.Random.Next(0, this.Individuals.Count())];
        }

        public Individual TournamentSelect(Graph graph, int size = 50, bool selectGood = true)
        {
            Individual best = this.RandomSelect();
            for (int i = 0; i < size; i++)
            {
                Individual selectedIndividual = this.RandomSelect();
                if (selectGood && (graph.Evaluate(selectedIndividual) < graph.Evaluate(best))
                    || (!selectGood && (graph.Evaluate(selectedIndividual) > graph.Evaluate(best))))
                {
                    best = selectedIndividual;
                }
            }

            return best;
        }

        public void Kill(Individual i)
        {
            this.Individuals.Remove(i);
        }

        public void Add(Individual i)
        {
            this.Individuals.Add(i);
        }

        public IEnumerator<Individual> GetEnumerator()
        {
            return ((IEnumerable<Individual>)Individuals).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Individual>)Individuals).GetEnumerator();
        }
    }
}
