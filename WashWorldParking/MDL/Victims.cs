using System;
namespace WashWorldParking.MDL
{
    public class Victims
    {
        public string lPlate { get; set; }
        public int index { get; set; }
        public string TypeOf { get; set; }

        public Victims(string lp, int id, string type)
        {
            lPlate = lp;
            index = id;
            TypeOf = type;
        }
    }
}
