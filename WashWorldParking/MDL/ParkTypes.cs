namespace WashWorldParking.MDL
{
    public abstract class ParkTypes
    { 
        public string FriendlyName { get; set; }
        public int BoxSize { get; set; }
        public decimal Price { get; set; }
        public bool Occupied { get; set; }
        public string ParkTime { get; set; } //TODO Ændre dette til DateTime i stedet for string... for fanden i helvede da også!
        public int BoxID { get; set; }
        public string LicensePlate { get; set; }

        public abstract void CalculateFee();
    }
}