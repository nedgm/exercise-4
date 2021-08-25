// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System.Collections.Generic;
using System.Text;

namespace TravellingSalesman
{
    public class Way
    {
        /// <summary>
        /// List of visited points
        /// </summary>
        protected List<int> Points { get; set; }

        /// <summary>
        /// Weight of way
        /// </summary>
        public double Weight { get; protected set; }

        public Way()
        {
            Weight = 0;
            Points = new List<int>();
        }

        public int GetPoint(int index) => Points[index];
        public void SetPoint(int index, int point) => Points[index] = point;
        public int FindPoint(int point) => Points.FindIndex(p => p == point);

        public void AddPoint(int point)
        {
            Points.Add(point);
        }

        public void RemovePoint(int point)
        {
            Points.Remove(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"[{Weight}]: ");
            sb.AppendJoin(" ", Points);

            return sb.ToString();
        }
    }
}