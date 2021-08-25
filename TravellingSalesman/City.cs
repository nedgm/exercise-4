// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;
using System.Collections.Generic;

namespace TravellingSalesman
{
    public class City
    {
        private static readonly int[,] defaultMatrix =
        {
            { 0, 1, 5, 2, 3, 2, 8 },
            { 1, 0, 1, 1, 2, 1, 3 },
            { 5, 1, 0, 2, 3, 5, 6 },
            { 2, 1, 2, 0, 1, 2, 3 },
            { 3, 2, 3, 1, 0, 1, 2 },
            { 2, 1, 5, 2, 1, 0, 1 },
            { 8, 3, 6, 3, 2, 1, 0 }
        };

        public int[,] Map { get; }

        public int PathCount { get; }

        public City() : this(defaultMatrix)
        {
        }

        public City(int[,] matrix)
        {
            Validate(matrix);

            Map = matrix;
            PathCount = CalculatePathCount(matrix);
        }

        public IList<int> GetInitialPath()
        {
            var initialPath = new List<int>();

            for (var i = 0; i < PathCount; i++)
            {
                initialPath.Add(i);
            }

            return initialPath;
        }

        private static void Validate(int[,] matrix)
        {
            if (matrix.Rank != 2)
            {
                throw new ArgumentException("Matrix rank must be equal to '2'.");
            }

            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new AggregateException("");
            }
        }

        private static int CalculatePathCount(int[,] matrix)
        {
            return (int) Math.Sqrt(matrix.Length);
        }
    }
}