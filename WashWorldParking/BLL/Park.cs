using System;
using System.Collections.Generic;
using WashWorldParking.MDL;
using WashWorldParking.REPO;

namespace WashWorldParking.BLL
{
    public class Park : iPark
    {
        public string ParkName { get; }
        public List<ParkTypes> Parkings { get; set; }

        private ParkTypes searchType;


        public Park(string name)
        {
            ParkName = name;
            Parkings = new List<ParkTypes>();
            HardCode();
        }

        /// <summary>
        /// Parkerer en bil
        /// </summary>
        /// <param name="input">Keypress</param>
        /// <param name="lPlate">Nummerplade</param>
        /// <returns>INT (0: OK - 1: Occupied - 2: lPlate exists!)</returns>
        public int ParkCar(ConsoleKeyInfo input, string lPlate)
        {
            if (CheckLicenseplate(lPlate)) return 2;
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    if (CheckAvailability(1)) return 1;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 1);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.ExpirationTime = DateTime.Now.AddHours(2).ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                case ConsoleKey.D2:
                    if (CheckAvailability(3)) return 1;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 3);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.ExpirationTime = DateTime.Now.AddHours(2).ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                case ConsoleKey.D3:
                    if (CheckAvailability(2)) return 1;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 2);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.ExpirationTime = DateTime.Now.AddHours(2).ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                case ConsoleKey.D4:
                    if (CheckAvailability(4)) return 1;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 4);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.ExpirationTime = DateTime.Now.AddHours(2).ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                default:
                    return 1;
            }
            return 0;
        }

        /// <summary>
        /// Tilføjer ekstra tid til sin parkering
        /// </summary>
        /// <param name="lPlate">Nummerplade</param>
        /// <param name="hours">INT/Tid i hele timer</param>
        /// <returns></returns>
        public string AddParkTime(string lPlate, int hours)
        {
            string result;
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            DateTime oldTime;
            DateTime.TryParse(searchType.ExpirationTime, out oldTime);
            searchType.ExpirationTime = oldTime.AddHours(hours).ToString();
            result = "New expiration time: " + searchType.ExpirationTime.ToString();
            return result;
        }

        public decimal RevokeTicket(string lPlate)
        { 
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            double diff = Math.Floor((DateTime.Parse(searchType.ExpirationTime) - DateTime.Now).TotalHours);
            if (diff > 1)
            {
                Console.WriteLine("Remaining parktime: " + diff);
                Console.Write("How many hours to you like to revoke: ");
                string revokeAmount = Console.ReadLine();
                try
                {
                    AddParkTime(lPlate, Convert.ToInt16(("-" + revokeAmount)));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please only input a number!");
                    Console.WriteLine(ex.Message);
                }
            }
            else if (diff < 1)
            {
                Console.WriteLine("You cannot revoke parktime. You doesn't have any remaining hours.");
                return 0;
            }

            if (searchType == null) throw new NullReferenceException("License plate doesn't exist");
            return 1;
        }

        public decimal CheckoutParking(string lPlate)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            decimal _ = searchType.CalculateFee();
            if (searchType == null) throw new NullReferenceException("License plate doesn't exist");
            return _;
        }

        private bool CheckLicenseplate(string lPlate)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            if (searchType == null) return false;
            return true;
        }

        /// <summary>
        /// Ser om en given p-type er ledig
        /// </summary>
        /// <param name="box">INT/Type</param>
        /// <returns>Boolean om der er optaget</returns>
        private bool CheckAvailability(int box)
        {
            int i = 0;
            foreach (var item in Parkings.FindAll(s => s.BoxSize == box))
            {
                if (!item.Occupied) i++ ;
            }
            if (i == 0) return true;
            return false;
        }

        public List<ParkTypes> GetParkTypes()
        {
            return null;
        }

        /// <summary>
        /// Hardcoded parkings TODO delete when file is filed
        /// </summary>
        private void HardCode()
        {
            int i = 1;
            for (int k = 0; k < 19; k++)
            {
                Parkings.Add(new CarPark(i));
                i++;
            }
            for (int k = 0; k < 4; k++)
            {
                Parkings.Add(new HandiPark(i));
                i++;
            }
            for (int k = 0; k < 10; k++)
            {
                Parkings.Add(new TruckPark(i));
                i++;
            }
            for (int k = 0; k < 3; k++)
            {
                Parkings.Add(new BusPark(i));
                i++;
            }
        }

        private void ParkLoader()
        { }
    }
}
