// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;
using System.Collections.Generic;

namespace TravellingSalesman.GeneticAlgorithm
{
    public class GeneticWay : Way
    {
        public GeneticWay(int pathCount, int[,] map)
        {
            for (var i = 0; i < pathCount; i++)
            {
                Points.Add(i);
            }

            GeneratePoints(pathCount);
            CountWeight(map);
        }

        public GeneticWay(List<int> points)
        {
            Points = points;
        }

        private void GeneratePoints(int pathCount)
        {
            var rand = new Random();

            var n = pathCount;
            while (n > 1)
            {
                n--;
                var i = rand.Next(n + 1);
                var temp = Points[i];
                Points[i] = Points[n];
                Points[n] = temp;
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

        /// <summary>
        /// Swapping point of two Ways.
        /// </summary>
        public void SwapPoints(GeneticWay way)
        {
            for (var i = 0; i < Points.Count - 1; i++)
            {
                var temp = Points[i];
                Points[i] = way.Points[i];
                way.Points[i] = temp;
            }
        }
    }
}