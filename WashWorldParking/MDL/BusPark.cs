using System;
namespace WashWorldParking.MDL
{
    public class BusPark : ParkTypes
    {
        public BusPark(int i)
        {
            FriendlyName = "Bus stop";
            BoxSize = 4;
            Price = 0;
            Occupied = false;
            ParkTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public BusPark(string fn, int bs, decimal pr, bool occ, string pt, int id)
        {
            FriendlyName = fn;
            BoxSize = bs;
            Price = pr;
            Occupied = occ;
            ParkTime = pt;
            BoxID = id;
        }

        public override void CalculateFee()
        {

        }
    }
}
