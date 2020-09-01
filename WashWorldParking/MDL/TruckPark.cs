using System;

namespace WashWorldParking.MDL
{
    public class TruckPark : ParkTypes
    {
        public TruckPark(int i)
        {
            FriendlyName = "Truck stop";
            BoxSize = 2;
            Price = 50;
            Occupied = false;
            ParkTime = "";
            ExpirationTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public TruckPark(string fn, int bs, decimal pr, bool occ, string pt, string et, int id, string lp)
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
            if (DateTime.Parse(ExpirationTime) < DateTime.Now) fee = 2000;
            var val = Math.Ceiling((DateTime.Parse(ExpirationTime) - DateTime.Parse(ParkTime)).TotalHours);
            if (val < 2) val = 2;
            return (Price * Convert.ToDecimal(val)) + fee;
        }
    }
}
