// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;

namespace TravellingSalesman.ProblemAntAlgorithm
{
    public class ProblemAntTravellingAlgorithm : TravellingAlgorithm
    {
        private int Good { get; set; }
        private double Avg { get; set; }
        private double Min { get; set; }

        private double Total { get; }
        private double Eps { get; }
        private double ControlValue { get; }
        private int TrainTimes { get; }

        public ProblemAntTravellingAlgorithm(City city) : base(city)
        {
            Good = 0;
            Avg = 0;
            Min = int.MaxValue;

            Total = 1000;
            Eps = 1;
            ControlValue = 10;
            TrainTimes = 25;
        }

        public override void Run()
        {
            Console.WriteLine("Problem Ant algorithm running...");

            for (var i = 0; i < Total; i++)
            {
                var graph = new Graph(city_.Map);
                graph.SetFeromones(1);

                var ant = new Ant(graph, 0, 1, 4, 0.3, 10, 2.5);
                ant.Train(TrainTimes);
                if (Math.Abs(ant.BestWay.Weight - ControlValue) < Eps)
                {
                    Good++;
                }

                Avg += ant.BestWay.Weight;

                if (Min > ant.BestWay.Weight)
                {
                    Min = ant.BestWay.Weight;
                }

                Console.WriteLine("Best way:");
                Console.WriteLine($"{ant.BestWay}");
            }

            Console.WriteLine($"Min: {Min}");
            Console.WriteLine($"Accur: {Good / Total}");
            Console.WriteLine($"Avg: {Avg / Total}");

            Console.WriteLine("Problem Ant algorithm finished.");
            Console.WriteLine();
        }
    }
}
