using System;
using TravellingSalesman.BruteForceAlgorithm;
using TravellingSalesman.GeneticAlgorithm;
using TravellingSalesman.ProblemAntAlgorithm;

namespace TravellingSalesman.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultCity = new City();

            Console.WriteLine("Press number to run algorithm:");
            Console.WriteLine("1: Brute Force");
            Console.WriteLine("2: Genetic");
            Console.WriteLine("3: Problem Ant");
            Console.WriteLine("Press 'ESC' to exit.");
            Console.WriteLine();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                ITravellingAlgorithm algorithm;
                switch (key.KeyChar)
                {
                    case '1':
                    {
                        algorithm = new BruteForceTravellingAlgorithm(defaultCity);
                        break;
                    }
                    case '2':
                        algorithm = new GeneticTravellingAlgorithm(defaultCity);
                        break;
                    case '3':
                        algorithm = new ProblemAntTravellingAlgorithm(defaultCity);
                        break;
                    default:
                        algorithm = null;
                        break;
                }

                algorithm?.Run();

            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
