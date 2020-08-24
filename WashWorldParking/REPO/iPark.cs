using System;
using System.Collections.Generic;
using WashWorldParking.MDL;

namespace WashWorldParking.REPO
{
    public interface iPark
    {
        public string ParkName { get; }
        public List<ParkTypes> GetParkTypes();
    }
}
