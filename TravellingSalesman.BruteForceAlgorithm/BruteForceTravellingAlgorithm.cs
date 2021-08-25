using System;
using System.Collections.Generic;

namespace TravellingSalesman.BruteForceAlgorithm
{
    public class BruteForceTravellingAlgorithm : TravellingAlgorithm
    {
        private static Way BestWay { get; set; }

        public BruteForceTravellingAlgorithm(City city) : base(city)
        {
        }

        public override void Run()
        {
            Console.WriteLine("Brute Force algorithm running...");

            RunInternal(initialPath => Generate(initialPath), out var time);
            RunInternal(initialPath => GenerateWithOptimization(initialPath), out var optTime);

            WriteStatistic(time, optTime);

            Console.WriteLine("Brute Force algorithm finished.");
            Console.WriteLine();
        }

        private static void RunInternal(Action<IList<int>> generate, out double time)
        {
            BestWay = new BruteForceWay(city_.PathCount, city_.Map);

            var initialPath = city_.GetInitialPath();

            var start = DateTime.Now;

            generate(initialPath);

            var finish = DateTime.Now - start;
            time = finish.TotalMilliseconds;
        }

        private static void WriteStatistic(double time, double optTime)
        {
            Console.WriteLine("Best way:");
            Console.WriteLine($"{BestWay}");
            Console.WriteLine($"Time spent on Program without optimization\t = {time} ms");
            Console.WriteLine($"Time spent on Program with optimization\t\t = {optTime} ms");
            Console.WriteLine();
        }

        /// <summary>
        /// Main recursive brute Force method. Iterates over all possible paths.
        /// </summary>
        /// <param name="pointer">Indicator for recursion</param>
        /// <param name="initialPath">Initial path</param>
        private static void Generate(IList<int> initialPath, int pointer = 0)
        {
            if (pointer == city_.PathCount - 1) //if we visited all cities
            {
                var way = new BruteForceWay();
                way.AddPoint(0);

                for (var i = 1; i < city_.PathCount; i++)
                {
                    way.AddPoint(initialPath[i]);
                }

                way.AddPoint(0);
                ResetBestWay(way);
            }
            else
            {
                //if there is some possible movements
                for (var j = pointer + 1; j < city_.PathCount; j++)
                {
                    Swap(initialPath, pointer + 1, j);
                    Generate(initialPath, pointer + 1);
                    Swap(initialPath, pointer + 1, j);
                }
            }
        }

        /// <summary>
        /// Main recursive brute Force method but optimized. Iterates over all possible paths.
        /// Saves time on adding new elements in list and appending best way.
        /// </summary>
        /// <param name="pointer">Indicator for recursion</param>
        /// <param name="initialPath">Initial path</param>
        private static void GenerateWithOptimization(IList<int> initialPath, int pointer = 0)
        {
            if (pointer == city_.PathCount - 1) //if we visited all cities
            {
                var way = new BruteForceWay();
                way.AddPoint(0);
                for (var i = 1; i < city_.PathCount; i++)
                {
                    way.AddPoint(initialPath[i]);
                    
                    if (i % 5 == 0 && way.CountWeight(city_.Map) > BestWay.Weight)
                        return;
                }

                way.AddPoint(0);
                ResetBestWay(way);
            }
            else
            {
                //if there is some possible movements
                for (var j = pointer + 1; j < city_.PathCount; j++)
                {
                    Swap(initialPath, pointer + 1, j);
                    Generate(initialPath, pointer + 1);
                    Swap(initialPath, pointer + 1, j);
                }
            }
        }

        /// <summary>
        /// Swaps two elements in initialPath
        /// </summary>
        /// <param name="firstIndex">First element</param>
        /// <param name="secondIndex">Second element</param>
        /// <param name="initialPath">List in which we swap elements</param>
        private static void Swap(IList<int> initialPath, int firstIndex, int secondIndex)
        {
            var t = initialPath[firstIndex];
            initialPath[firstIndex] = initialPath[secondIndex];
            initialPath[secondIndex] = t;
        }

        /// <summary>
        /// Compares parameter way and best way. Changes best way if needed.
        /// </summary>
        /// <param name="way"></param>
        private static void ResetBestWay(BruteForceWay way)
        {
            if (BestWay.Weight > way.CountWeight(city_.Map))
            {
                BestWay = way;
                //BestWay.FullPrint();            //Prints best way if it was appended
            }
        }
    }
}
