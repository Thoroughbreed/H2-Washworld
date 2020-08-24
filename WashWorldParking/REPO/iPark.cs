using System;
using System.Collections.Generic;

namespace WashWorldParking.REPO
{
    public interface iPark
    {
        public string ParkName { get; }
        public List<ParkTypes> GetParkTypes();
    }
}
