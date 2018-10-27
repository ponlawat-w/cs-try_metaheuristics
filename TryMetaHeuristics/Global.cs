using System;
using System.Collections.Generic;
using System.Text;

namespace TryMetaHeuristics
{
    public static class Global
    {
        public static Random Random = new Random();

        public static double NormalRandom(double mean, double std)
        {
            double u1 = 1.0 - Random.NextDouble();
            double u2 = 1.0 - Random.NextDouble();
            double normalRand = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + (std * normalRand);
        }
    }
}
