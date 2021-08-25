// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;

namespace TravellingSalesman.BruteForceAlgorithm
{
    public class BruteForceWay : Way
    {
        public BruteForceWay()
        {
        }

        public BruteForceWay(int pathCount, int[,] map)
        {
            for (var i = 0; i < pathCount + 1; i++)
            {
                Points.Add(-1);
            }

            GeneratePoints(pathCount);
            CountWeight(map);
        }

        /// <summary>
        /// Generates random Hamiltonian cycle 
        /// </summary>
        /// <param name="pathCount">Path count</param>
        private void GeneratePoints(int pathCount)
        {
            var rand = new Random();

            Points[0] = 0;
            Points[pathCount] = 0;

            for (var i = 1; i < pathCount; i++)
            {
                var index = rand.Next(pathCount) + 1;

                while (Points[index] != -1)
                {
                    index = rand.Next(pathCount) + 1;
                }

                Points[index] = i;
            }
        }

        /// <summary>
        /// Counts full weight of the way
        /// </summary>
        public double CountWeight(int[,] map)
        {
            Weight = 0;
            for (var i = 0; i < Points.Count - 1; i++)
            {
                var mock = map[Points[i], Points[i + 1]];
                Weight += mock;
            }

            return Weight;
        }
    }
}