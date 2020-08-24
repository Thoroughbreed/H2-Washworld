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
            BoxID = i;
            LicensePlate = "";
        }

        public CarPark(string fn, int bs, decimal pr, bool occ, string pt, int id)
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
