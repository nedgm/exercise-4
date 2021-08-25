// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;

namespace TravellingSalesman.ProblemAntAlgorithm
{
    public class Graph
    {
        public Edge[,] Edges { get; }

        public int Size { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="map">Distance matrix</param>
        public Graph(int[,] map)
        {
            Size = (int) Math.Sqrt(map.Length);
            Edges = new Edge[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = i + 1; j < Size; j++)
                {
                    Edges[i, j] = new Edge(map[i, j]);
                    Edges[j, i] = new Edge(map[i, j]);
                }
            }
        }

        /// <summary>
        /// Set feromone for each edge
        /// </summary>
        /// <param name="startFeromone">Start amount of feromone</param>
        public void SetFeromones(double startFeromone)
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = i + 1; j < Size; j++)
                {
                    Edges[i, j].Feromone = startFeromone;
                    Edges[j, i].Feromone = startFeromone;
                }
            }
        }
    }
}