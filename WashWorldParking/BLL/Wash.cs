using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.MDL;
using WashWorldParking.REPO;
using WashWorldParking.UTIL;

namespace WashWorldParking.BLL
{ 
    public class Wash : iWash
    {
        public string WashName { get; }
        public List<WashMembers> Members;
        private List<WashTypes> Washes;
        private WashMembers searchType;

        public Wash(string name)
        {
            WashName = name;
            Task loadW = Task.Factory.StartNew(() => BeginWashThingy());
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Loading washingmachine - please wait");
            while (!loadW.IsCompleted)
            {
                Thread.Sleep(250);
            }
            Console.WriteLine("Washingmachine loaded.");
        }

        private void BeginWashThingy()
        {
            Members = new List<WashMembers>();
            Washes = new List<WashTypes>();
            for (int i = 0; i < 3; i++)
            {
                Washes.Add(new WashTypes(i+1));
            }
            Members = FileLogger.ReadFromWash();
            Thread.Sleep(750); 
        }

        public string GetWashTypes()
        {
            string result = "";
            foreach (var item in Washes)
            {
                if (item.Busy)
                {
                    result += $"Wash ID: {item.WashID} | The wash is busy\n";
                }
                result += $"Wash ID: {item.WashID} | The wash is available\n";
            }
            return result;
        }

        public string WashCar(string lPLate)
        {
            return GetWashTypes();
        }

        public bool CreateAccount(string lPlate, string cCard, string eMail, int wType)
        {
            bool result = false;
            try
            {
                if (CheckLicenseplate(lPlate)) throw new LPlateSubscribed();
                Members.Add(new WashMembers(lPlate, cCard, eMail, wType));
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something weird happened.");
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public bool CheckLicenseplate(string lPlate)
        {
            searchType = Members.Find(s => s.LPlate == lPlate);
            if (searchType == null) return false;
            return true;
        }
    }
}
