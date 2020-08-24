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


        public Park(string name)
        {
            ParkName = name;
            HardCode();
        }

        public void ParkCar(ConsoleKeyInfo input, string lPlate)
        {
            ParkTypes searchType;
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 1);
                    searchType.ParkTime = DateTime.Now.ToLongDateString();
                    searchType.Occupied = true;
                    searchType.LicensePlate = lPlate;
                    break;
                case ConsoleKey.D2:
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 3);
                    searchType.ParkTime = DateTime.Now.ToLongDateString();
                    searchType.Occupied = true;
                    searchType.LicensePlate = lPlate;
                    break;
                case ConsoleKey.D3:
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 2);
                    searchType.ParkTime = DateTime.Now.ToLongDateString();
                    searchType.Occupied = true;
                    searchType.LicensePlate = lPlate;
                    break;
                case ConsoleKey.D4:
                    searchType = Parkings.Find(s => s.Occupied == false && s.BoxSize == 4);
                    searchType.ParkTime = DateTime.Now.ToLongDateString();
                    searchType.Occupied = true;
                    searchType.LicensePlate = lPlate;
                    break;

                default:
                    break;
            }
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
            for (int k = 0; k < 8; k++)
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
