using System.Collections.Generic;

namespace TravellingSalesman
{
    public static class CityNameMapper
    {
        private static int _unknownCityCounter;
        private static Dictionary<int, string> _values = new Dictionary<int, string>
        {
            {0, "A" },
            {1, "B" },
            {2, "C" },
            {3, "D" },
            {4, "E" },
            {5, "F" },
            {6, "G" }
        };

        public static string Map(int index)
        {
            if (_values.TryGetValue(index, out var value))
            {
                return value;
            }

            return "Unknown city " + _unknownCityCounter++;
        }
    }
}