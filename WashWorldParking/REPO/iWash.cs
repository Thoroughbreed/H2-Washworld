using System;
using System.Collections.Generic;

namespace WashWorldParking.REPO
{
    public interface iWash
    {
        public string WashName { get; }
        public List<WashTypes> GetWashTypes();
    }
}
