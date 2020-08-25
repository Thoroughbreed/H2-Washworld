using System;

namespace WashWorldParking.MDL
{
    public class CarPark : ParkTypes
    {
        public CarPark(int i)
        {
            FriendlyName = "Carpark";
            BoxSize = 1;
            Price = 5;
            Occupied = false;
            ParkTime = "";
            ExpirationTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public CarPark(string fn, int bs, decimal pr, bool occ, string pt, string et, int id)
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
            var val = Math.Ceiling((DateTime.Parse(ExpirationTime) - DateTime.Parse(ParkTime)).TotalHours);
            if (val < 2) val = 2;
            return (Price * Convert.ToDecimal(val));
        }
    }
}
