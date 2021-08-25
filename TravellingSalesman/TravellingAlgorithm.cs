// Copyright (C) CompatibL Technologies LLC. All rights reserved.
// This code contains valuable trade secrets and may be used, copied,
// stored, or distributed only in accordance with a written license
// agreement and with the inclusion of this copyright notice.

namespace TravellingSalesman
{
    public abstract class TravellingAlgorithm : ITravellingAlgorithm
    {
        protected static City city_;

        protected TravellingAlgorithm(City city)
        {
            city_ = city;
        }

        public abstract void Run();
    }
}