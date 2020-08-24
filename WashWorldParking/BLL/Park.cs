using System;
using System.Collections.Generic;
using WashWorldParking.REPO;

namespace WashWorldParking.BLL
{
    public class Park : iPark
    {
        public string ParkName { get; }

        public Park(string name)
        {
            ParkName = name;
        }

        public List<ParkTypes> GetParkTypes()
        {
            return null;
        }

    }
}
