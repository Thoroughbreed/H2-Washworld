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
            BoxID = i;
            LicensePlate = "";
        }

        public HandiPark(string fn, int bs, decimal pr, bool occ, string pt, int id)
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
