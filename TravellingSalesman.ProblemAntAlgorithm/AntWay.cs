// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

using System.Collections.Generic;

namespace TravellingSalesman.ProblemAntAlgorithm
{
    public class AntWay : Way
    {
        public AntWay()
        {
            Weight = double.MaxValue;
        }

        public AntWay(double weight, List<int> points)
        {
            Weight = weight;
            Points = points;
        }
    }
}