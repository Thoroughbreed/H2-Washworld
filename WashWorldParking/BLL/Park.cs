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
        /// Parks vehicle
        /// </summary>
        /// <param name="input">Keypress</param>
        /// <param name="lPlate"></param>
        /// <returns>INT (0: OK - 1: Occupied - 2: lPlate exists!)</returns>
        public int ParkCar(ConsoleKeyInfo input, string lPlate)
        {
            // Returns "Exist" if the plate is already parked
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
        /// Adds time to parking ticket
        /// </summary>
        /// <param name="lPlate"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public string AddParkTime(string lPlate, int hours)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            DateTime oldTime;
            DateTime.TryParse(searchType.ExpirationTime, out oldTime);
            searchType.ExpirationTime = oldTime.AddHours(hours).ToString();
            return string.Format($"New expiration time: {searchType.ExpirationTime}");
        }

        /// <summary>
        /// Revokes time from parking ticket, and gets a "refund"
        /// </summary>
        /// <param name="lPlate"></param>
        /// <returns></returns>
        public string RevokeTicket(string lPlate)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            double diff = Math.Floor((DateTime.Parse(searchType.ExpirationTime) - DateTime.Now).TotalHours);
            if (diff > 1)
            {
                Console.WriteLine($"Remaining parktime: {diff}");
                Console.Write("How many hours to you like to revoke: ");
                string revokeAmount = Console.ReadLine();
                try
                {
                    int _ = Convert.ToInt16(revokeAmount);
                    if (_ > 0)
                    {
                        if (_ >= diff) throw new BadUser();
                        AddParkTime(lPlate, Convert.ToInt16(("-" + revokeAmount)));
                    }
                    else if (_ < 1)
                    {
                        throw new BadUser();
                    }
                    return string.Format($"You've successfully revoked {revokeAmount} hours. Your new expiration time is: {searchType.ExpirationTime}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please only input a number!");
                    Console.WriteLine(ex.Message);
                }
            }
            else if (diff < 2)
            {
                return string.Format("You cannot revoke parktime. You doesn't have any remaining hours.");
            }

            if (searchType == null) throw new NullReferenceException();
            return null;
        }

        /// <summary>
        /// Pays for parking (and removes car from parkinglot
        /// </summary>
        /// <param name="lPlate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if plate exists
        /// </summary>
        /// <param name="lPlate"></param>
        /// <returns></returns>
        private bool CheckLicenseplate(string lPlate)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            if (searchType == null) return false;
            return true;
        }

        /// <summary>
        /// Checks if the parling space/type is available
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Super secret admin update tool!
        /// </summary>
        /// <param name="lp"></param>
        public void AdminUpd(string lp, string pt, string et, decimal price)
        {
            searchType = Parkings.Find(s => s.LicensePlate == lp);
            searchType.LicensePlate = lp;
            searchType.ParkTime = pt;
            searchType.ExpirationTime = et;
            searchType.Price = price;
        }

        private void ParkLoader()
        {
            Parkings = FileLogger.ReadFromPark();
            Thread.Sleep(2500); //Virtual wait on load
        }
    }
}
