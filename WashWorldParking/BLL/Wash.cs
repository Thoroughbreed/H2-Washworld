using System;
using System.Collections.Generic;
using WashWorldParking.REPO;

namespace WashWorldParking.BLL
{ 
    public class Wash : iWash
    {
        public string WashName { get; }
        public Wash(string name)
        {
            WashName = name;
        }

        public List<WashTypes> GetWashTypes()
        {
            return null;
        }
    }
}
