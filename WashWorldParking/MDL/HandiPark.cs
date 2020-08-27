using System;
namespace WashWorldParking.MDL
{
    public class HandiPark : ParkTypes
    {
        public HandiPark(int i)
        {
            FriendlyName = "Handicap parking";
            BoxSize = 3;
            Price = 0;
            Occupied = false;
            ParkTime = "";
            ExpirationTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public HandiPark(string fn, int bs, decimal pr, bool occ, string pt, string et, int id, string lp)
        {
            FriendlyName = fn;
            BoxSize = bs;
            Price = pr;
            Occupied = occ;
            ParkTime = pt;
            ExpirationTime = et;
            BoxID = id;
            LicensePlate = lp;
        }

        public override decimal CalculateFee()
        {
            return Price;
        }
    }
}
