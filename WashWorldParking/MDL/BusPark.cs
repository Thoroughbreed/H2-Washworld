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
            ExpirationTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public BusPark(string fn, int bs, decimal pr, bool occ, string pt, string et, int id)
        {
            FriendlyName = fn;
            BoxSize = bs;
            Price = pr;
            Occupied = occ;
            ParkTime = pt;
            ExpirationTime = et;
            BoxID = id;
        }

        public override decimal CalculateFee()
        {
            var val = Math.Ceiling((DateTime.Now - DateTime.Parse(ParkTime)).TotalHours);
            if (val < 2) val = 2;
            return (Price * Convert.ToDecimal(val));
        }
    }
}
