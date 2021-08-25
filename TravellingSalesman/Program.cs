using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TravellingSalesman
{
    class Program
    {
        static void Main()
        {
            try
            {
                var distanceMatrix = InitializeDistanceMatrix();
                var initialSubpath = new List<int>();
                for (int i = 1; i < distanceMatrix.GetLength(0); i++)
                {
                    initialSubpath.Add(i);
                }

                var subpaths = GetAllPermutations(initialSubpath);
                var fullPaths = GetFullPaths(subpaths);
                var result = FindOptimalPaths(fullPaths, distanceMatrix);
                FormatOutput(result);
            }
            catch (FormatException e)
            {
                File.WriteAllText(@"..\..\..\Result.txt", e.Message);
            }
            catch (Exception e)
            {
                File.WriteAllText(@"..\..\..\Result.txt", "An unexpected error occured. Please check out the log file for more information");
                File.WriteAllLines(@"..\..\..\Log.txt", new List<string> { e.Message, e.StackTrace });
            }
        }

        private static int[,] InitializeDistanceMatrix()
        {
            var lines = File.ReadAllLines(@"..\..\..\DistanceMatrix.txt");
            if (lines.Length == 0)
            {
                throw new FormatException("The distance matrix is empty. Please correct your input and try again");
            }
            var matrixOrder = lines.Length;
            var result = new int[matrixOrder, matrixOrder];
            var i = 0;
            foreach (var line in lines)
            {
                var lineValues = line.Split(',');
                if (lineValues.Length != matrixOrder)
                {
                    throw new FormatException("The distance matrix is not a square matrix. Please correct your input and try again");
                }

                var j = 0;
                foreach (var lineValue in lineValues)
                {
                    if (int.TryParse(lineValue, out var cost))
                    {
                        result[i, j] = cost;
                    }
                    else
                    {
                        throw new FormatException($"The distance matrix contains a non-numeric element at [{i + 1}, {j + 1}] position. Please correct your input and try again");
                    }
                    j++;
                }
                i++;
            }

            return result;
        }

        private static IReadOnlyCollection<IReadOnlyCollection<int>> GetAllPermutations(List<int> set)
        {
            var result = new List<List<int>>();
            set.ForEach(item => result.Add(new List<int> { item }));

            while (result.Any(_ => _.Count != set.Count))
            {
                var permutationsToDelete = new List<List<int>>();
                var permutationsToAdd = new List<List<int>>();
                foreach (var permutation in result)
                {
                    var availableItems = set.Except(permutation);
                    foreach (var availableItem in availableItems)
                    {
                        var permutationToAdd = permutation.Append(availableItem).ToList();
                        permutationsToAdd.Add(permutationToAdd);

                    }
                    permutationsToDelete.Add(permutation);
                }
                permutationsToDelete.ForEach(_ => result.Remove(_));
                permutationsToAdd.ForEach(_ => result.Add(_));
            }

            return result;
        }

        private static IReadOnlyCollection<IReadOnlyCollection<int>> GetFullPaths(IReadOnlyCollection<IReadOnlyCollection<int>> subpaths)
        {
            var result = new List<List<int>>();
            foreach (var subpath in subpaths)
            {
                var fullPath = new List<int>();
                fullPath.Add(0);
                fullPath.AddRange(subpath);
                fullPath.Add(0);

                result.Add(fullPath);
            }

            return result;
        }

        private static (IReadOnlyCollection<IReadOnlyCollection<int>>, int) FindOptimalPaths(IReadOnlyCollection<IReadOnlyCollection<int>> paths, int[,] distanceMatrix)
        {
            int? optimalCost = null;
            var optimalPaths = new List<IReadOnlyCollection<int>>();

            foreach (var path in paths)
            {
                var cost = 0;
                for (var i = 0; i < path.Count - 1; i++)
                {
                    cost += distanceMatrix[path.ElementAt(i), path.ElementAt(i + 1)];
                    if (optimalCost.HasValue && cost > optimalCost)
                    {
                        break;
                    }
                }

                if (optimalCost == null || cost < optimalCost.Value)
                {
                    optimalCost = cost;
                    optimalPaths = new List<IReadOnlyCollection<int>> { path };
                }
                else if (cost == optimalCost.Value)
                {
                    optimalPaths.Add(path);
                }
            }

            return (optimalPaths, optimalCost.GetValueOrDefault());
        }

        private static void FormatOutput((IReadOnlyCollection<IReadOnlyCollection<int>>, int) result)
        {
            var pathOutputSubstring = "Optimal paths: " + Environment.NewLine;
            foreach (var path in result.Item1)
            {
                pathOutputSubstring += string.Join(" " + '\u2192' + " ", path.Select(CityNameMapper.Map));
                pathOutputSubstring += Environment.NewLine;
            }

            File.WriteAllLines(@"..\..\..\Result.txt", new List<string> { pathOutputSubstring, $"Total cost: {result.Item2}" });
        }
    }
}
