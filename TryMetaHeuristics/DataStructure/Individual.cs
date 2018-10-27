using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TryMetaHeuristics.DataStructure
{
    public class Individual
    {
        public Visiting[] Visitings;

        private Individual()
        {
        }

        public Individual(int size)
        {
            this.Visitings = new Visiting[size];
            for (int i = 0; i < size; i++)
            {
                this.Visitings[i] = new Visiting(i, this.GetWeightsBefore(i));
            }
        }

        public double[] GetWeightsBefore(int index)
        {
            return this.Visitings.Take(index)
                .Select(v => v.Weight)
                .ToArray();
        }

        public void Tweak(double mean = 0.0, double std = 0.01)
        {
            for (int i = 0; i < this.Visitings.Length; i++)
            {
                double newWeight;
                do
                {
                    newWeight = this.Visitings[i].Weight
                        + Global.NormalRandom(mean, std);
                }
                while (this.GetWeightsBefore(i).Contains(newWeight)
                    || newWeight < 0.0 || newWeight > 1.0);

                this.Visitings[i].Weight = newWeight;
            }
        }

        public Individual Clone()
        {
            return new Individual
            {
                Visitings = this.Visitings.Select(v => v.Clone()).ToArray()
            };
        }
        
    }
}
