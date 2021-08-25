// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

namespace TravellingSalesman.ProblemAntAlgorithm
{
    public class Edge
    {
        public double Feromone { get; set; }
        public double Weight { get; }
        public double DFeromone { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="weight">Edge weight</param>
        /// <param name="feromone">Edge feromone (default = 0)</param>
        public Edge(double weight, double feromone = 0)
        {
            Weight = weight;
            Feromone = feromone;
            DFeromone = 0;
        }
    }
}