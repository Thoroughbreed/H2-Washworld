using System;
namespace WashWorldParking.MDL
{
    public class WashMembers
    {
        public string LPlate { get; set; }
        public string CCard { get; set; }
        public string EMail { get; set; }
        public int WashType { get; set; }
        public string WashName { get; set; }

        public WashMembers(string lPlate, string cCard, string eMail, int wType)
        {
            LPlate = lPlate;
            CCard = cCard;
            EMail = eMail;
            WashType = wType;
            if (wType == 1) WashName = "Bronze wash";
            if (wType == 2) WashName = "Silver wash";
            if (wType == 3) WashName = "Golden shower";
        }
    }
}
