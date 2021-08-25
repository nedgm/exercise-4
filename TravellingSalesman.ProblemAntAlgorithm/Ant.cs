// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System;
using System.Collections.Generic;

namespace TravellingSalesman.ProblemAntAlgorithm
{
    public class Ant
    {
        public AntWay BestWay { get; private set; }

        private Graph Graph { get; }
        private int StartNode { get; }
        private double Alfa { get; }
        private double Beta { get; }
        private double P { get; }
        private int AntCount { get; }
        private double FeromoneCoef { get; }

        public Ant(Graph graph, int startNode, double alfa, double beta, double p, int antCount, double feromoneCoef)
        {
            BestWay = new AntWay();

            Graph = graph;
            StartNode = startNode;
            Alfa = alfa;
            Beta = beta;
            P = p;
            AntCount = antCount;
            FeromoneCoef = feromoneCoef;
        }

        /// <summary>
        /// Start simulation
        /// </summary>
        /// <param name="times">Number of iterations</param>
        public void Train(int times = 10)
        {
            var rnd = new Random();

            for (var t = 0; t < times; t++)
            {
                for (var ant = 0; ant < AntCount; ant++)
                {
                    Move(rnd);
                }
                RefreshFeromones();
            }
            RebuiltWay();
        }

        /// <summary>
        /// Refresh all edge's feromones after iteration
        /// </summary>
        private void RefreshFeromones()
        {
            for (var i = 0; i < Graph.Size; i++)
            {
                for (var j = i + 1; j < Graph.Size; j++)
                {
                    Graph.Edges[i, j].Feromone *= (1 - P);
                    Graph.Edges[i, j].Feromone += Graph.Edges[i, j].DFeromone;
                    Graph.Edges[i, j].DFeromone = 0;

                    Graph.Edges[j, i].Feromone *= (1 - P);
                    Graph.Edges[j, i].Feromone += Graph.Edges[j, i].DFeromone;
                    Graph.Edges[j, i].DFeromone = 0;
                }
            }
        }

        /// <summary>
        /// Simulate one ant's route from random node. Calculate feromone additive for each edge
        /// </summary>
        /// <returns>(Ant's route node sequence, route weight)</returns>
        private void Move(Random rnd)
        {
            var totalWeight = 0.0;
            var visitedNodes = new List<int>();
            var availableNodes = new List<int>();

            var startNode = rnd.Next(Graph.Size);
            visitedNodes.Add(startNode);

            for (var i = 0; i < Graph.Size; i++)
            {
                if (i != startNode)
                {
                    availableNodes.Add(i);
                }
            }

            while (availableNodes.Count != 0)
            {
                var total = 0.0;
                var curNode = visitedNodes[^1];

                foreach (var node in availableNodes)
                {
                    var edge = Graph.Edges[curNode, node];
                    total += Math.Pow(edge.Feromone, Alfa) / Math.Pow(edge.Weight, Beta);
                }

                var stopValue = rnd.NextDouble();
                var curValue = 0.0;
                foreach (var node in availableNodes)
                {
                    var edge = Graph.Edges[curNode, node];
                    curValue += Math.Pow(edge.Feromone, Alfa) / Math.Pow(edge.Weight, Beta) / total;

                    if (stopValue <= curValue)
                    {
                        totalWeight += edge.Weight;
                        curNode = node;
                        break;
                    }
                }

                visitedNodes.Add(curNode);
                availableNodes.Remove(curNode);
            }

            var lastEdge = Graph.Edges[visitedNodes[0], visitedNodes[Graph.Size - 1]];
            visitedNodes.Add(visitedNodes[0]);
            totalWeight += lastEdge.Weight;

            for (var i = 1; i < visitedNodes.Count; i++)
            {
                Graph.Edges[visitedNodes[i - 1], visitedNodes[i]].DFeromone += FeromoneCoef / totalWeight;
                Graph.Edges[visitedNodes[i], visitedNodes[i - 1]].DFeromone += FeromoneCoef / totalWeight;
            }

            if (totalWeight < BestWay.Weight)
            {
                BestWay = new AntWay(totalWeight, visitedNodes);
            }
        }

        /// <summary>
        /// Rebuild route to start from startNode
        /// </summary>
        public void RebuiltWay()
        {
            if (BestWay.GetPoint(0) != StartNode)
            {
                BestWay.RemovePoint(BestWay.GetPoint(0));

                while (BestWay.GetPoint(0) != StartNode)
                {
                    var t = BestWay.GetPoint(0);
                    BestWay.RemovePoint(t);
                    BestWay.AddPoint(t);
                }

                BestWay.AddPoint(StartNode);
            }

        }
    }
}