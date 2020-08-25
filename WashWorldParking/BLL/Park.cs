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

        public bool ParkCar(ConsoleKeyInfo input, string lPlate)
        {
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    if (CheckAvailability(1)) return true;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 1);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                case ConsoleKey.D2:
                    if (CheckAvailability(3)) return true;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 3);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                case ConsoleKey.D3:
                    if (CheckAvailability(2)) return true;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 2);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                case ConsoleKey.D4:
                    if (CheckAvailability(4)) return true;
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 4);
                    searchType.ParkTime = DateTime.Now.ToString();
                    searchType.LicensePlate = lPlate;
                    searchType.Occupied = true;
                    break;
                default:
                    break;
            }
            return false;
        }

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

        public string AddParkTime(string lPlate, int hours)
        {
            string result;
            searchType = Parkings.Find(s => s.LicensePlate == lPlate);
            DateTime oldTime;
            DateTime.TryParse(searchType.ParkTime, out oldTime);
            searchType.ParkTime = oldTime.AddHours(hours).ToString();
            result = "New expiration time: " + searchType.ParkTime.ToString();
            return result;
        }

        public List<ParkTypes> GetParkTypes()
        {
            return null;
        }

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

    }
}
