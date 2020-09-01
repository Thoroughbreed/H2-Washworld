using System;

namespace WashWorldParking.MDL
{
    public class BusPark : ParkTypes
    {
        public BusPark(int i)
        {
            FriendlyName = "Bus stop";
            BoxSize = 4;
            Price = 20;
            Occupied = false;
            ParkTime = "";
            ExpirationTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public BusPark(string fn, int bs, decimal pr, bool occ, string pt, string et, int id, string lp)
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
            decimal fee = 0;
            if (DateTime.Parse(ExpirationTime) < DateTime.Now) fee = 1000;
            double val = Math.Ceiling((DateTime.Parse(ExpirationTime) - DateTime.Parse(ParkTime)).TotalHours);
            if (val < 2) val = 2;
            return (Price * Convert.ToDecimal(val)) + fee;
        }
    }
}
