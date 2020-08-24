namespace WashWorldParking.MDL
{
    public class TruckPark : ParkTypes
    {
        public TruckPark(int i)
        {
            FriendlyName = "Truck stop";
            BoxSize = 2;
            Price = 20;
            Occupied = false;
            ParkTime = "";
            BoxID = i;
            LicensePlate = "";
        }

        public TruckPark(string fn, int bs, decimal pr, bool occ, string pt, int id)
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
