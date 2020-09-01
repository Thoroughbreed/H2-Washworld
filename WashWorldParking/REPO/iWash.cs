using System.Collections.Generic;
using WashWorldParking.MDL;

namespace WashWorldParking.REPO
{
    public interface iWash
    {
        public string WashName { get; }
        public List<WashMembers> Members { get; set; }
        public List<WashTypes> Washes { get; set; }

        public bool CreateAccount(string lPlate, string cCard, string eMail, int wType);
        public bool CheckLicenseplate(string lPlate);
        public int GetMemberWashType(string lPlate);
    }
}
