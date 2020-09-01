using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.MDL;
using WashWorldParking.REPO;
using WashWorldParking.UTIL;

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
            //HardCode();
            Task loadP = Task.Factory.StartNew (() => ParkLoader());
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Loading parkinglot - please wait");
            while (!loadP.IsCompleted)
            {
                Thread.Sleep(250);
            }
            Console.WriteLine("Parkinglot loaded.");
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

        public void RevokeTicket(string lPlate)
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
                    int _ = Convert.ToInt16(revokeAmount);
                    if (_ > 0)
                    {
                        AddParkTime(lPlate, Convert.ToInt16(("-" + revokeAmount)));
                    }
                    else if (_ < 1)
                    {
                        throw new BadUser();
                    }
                    Console.WriteLine("You've successfully revoked {0} hours. Your new expiration time is: {1}", revokeAmount, searchType.ExpirationTime);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please only input a number!");
                    Console.WriteLine(ex.Message);
                }
            }
            else if (diff < 2)
            {
                Console.WriteLine("You cannot revoke parktime. You doesn't have any remaining hours.");
            }

            if (searchType == null) throw new NullReferenceException();
        }

        public decimal CheckoutParking(string lPlate)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            decimal _ = searchType.CalculateFee();
            if (searchType == null) throw new NullReferenceException();
            if (searchType != null)
            {
                searchType.ExpirationTime = "";
                searchType.LicensePlate = "";
                searchType.Occupied = false;
                searchType.ParkTime = "";
            }
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

        private void ParkLoader()
        {
            Parkings = FileLogger.ReadFromPark();
            Thread.Sleep(2500); //Virtuel ventetid på load
        }
    }
}
