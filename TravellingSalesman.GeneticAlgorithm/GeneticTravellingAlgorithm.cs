// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;
using System.Collections.Generic;
using System.Linq;

namespace TravellingSalesman.GeneticAlgorithm
{
    public class GeneticTravellingAlgorithm : TravellingAlgorithm
    {
        private int PathCount { get; }
        private int BestCount { get; }
        private int Ticks { get; }

        public GeneticTravellingAlgorithm(City city) : base(city)
        {
            PathCount = 24;
            BestCount = 8;
            Ticks = 20;
        }

        public override void Run()
        {
            Console.WriteLine("Genetic algorithm running...");

            var ways = CreateWays();

            for (var i = 0; i < Ticks; i++)
            {
                MutateWays(ways);
                ways.ForEach(w => w.CountWeight(city_.Map));
                ways = SortWays(ways);
            }

            var bestWay = FindBestWay(ways);
            bestWay.CountWeight(city_.Map);

            Console.WriteLine("Best way:");
            Console.WriteLine($"{bestWay}");
            Console.WriteLine();

            Console.WriteLine("Genetic algorithm finished.");
            Console.WriteLine();
        }

        private static GeneticWay FindBestWay(IList<GeneticWay> ways)
        {
            var way = ways[0];
            var index = way.FindPoint(0);
            var points = new List<int>();

            for (var i = index; i < city_.PathCount; i++)
            {
                points.Add(way.GetPoint(i));
            }

            for (var i = 0; i < index; i++)
            {
                points.Add(way.GetPoint(i));
            }

            points.Add(0);

            return new GeneticWay(points);
        }

        private List<GeneticWay> CreateWays()
        {
            var ways = new List<GeneticWay>();

            for (var i = 0; i < PathCount; i++)
            {
                var way = new GeneticWay(city_.PathCount, city_.Map);
                ways.Add(way);
            }

            return SortWays(ways);
        }

        private List<GeneticWay> SortWays(IList<GeneticWay> ways)
        {
            return ways.OrderBy(w => w.Weight).ToList();
        }

        private void MutateWays(IList<GeneticWay> ways)
        {
            var random = new Random();

            for (var i = 0; i < BestCount; i++)
            {
                ways[i].SwapPoints(ways[i]);
            }

            for (var i = BestCount; i < PathCount; i++)
            {
                for (var j = 0; j < city_.PathCount; j++)
                {
                    var copyPoint = ways[i % BestCount].GetPoint(j);
                    ways[i].SetPoint(j, copyPoint);
                }

                var temp1 = random.Next(city_.PathCount);
                var temp2 = random.Next(city_.PathCount);

                while (temp2 == temp1)
                {
                    temp2 = random.Next(city_.PathCount);
                }

                var copyPoint1 = ways[i].GetPoint(temp1);
                var copyPoint2 = ways[i].GetPoint(temp2);
                ways[i].SetPoint(temp1, copyPoint2);
                ways[i].SetPoint(temp2, copyPoint1);
            }
        }
    }
}
