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
        public List<WashTypes> Washes;
        private WashMembers searchType;
        public CancellationTokenSource HALT { get; set; }

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
            HALT = new CancellationTokenSource();
            for (int i = 0; i < 3; i++)
            {
                Washes.Add(new WashTypes(i+1));
            }
            Members = FileLogger.ReadFromWash();
            Thread.Sleep(750); 
        }

        public string GetWashTypes()
        {
            string result = "There is no washs available at the moment. 🥺";
            foreach (var item in Washes)
            {
                if (!item.Busy)
                {
                    result += $"[{item.WashID}] Wash ID: {item.WashID} | The wash is available\n";
                }
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

        public int GetMemberWashType (string lPlate)
        {
            searchType = Members.Find(s => s.LPlate == lPlate);
            return searchType.WashType;

        }

        public decimal[] StartWash(int type, bool member)
        {
            decimal[] result = new decimal[2];
            WashTypes find = Washes.Find(s => s.Busy == false);
            if (find == null) throw new NoWash();
            result[0] = find.WashID;
            result[1] = find.WashNow(type, member, HALT);
            find.W.Start();
            return result;
        }

        public void StatusText()
        {
            foreach (var id in Washes)
            {
                if (id.Busy)
                {
                    Console.SetCursorPosition(30, id.WashID * 2);
                    Console.WriteLine($"[{id.WashID}] The wash is currently busy");
                    Console.SetCursorPosition(30, (id.WashID * 2) + 1);
                    Console.WriteLine("It is currently: " + id.FriendlyName);
                }
                else
                {
                    Console.SetCursorPosition(30, id.WashID * 2);
                    Console.WriteLine($"[{id.WashID}] The wash is available");
                    Console.SetCursorPosition(30, (id.WashID * 2) + 1);
                    Console.WriteLine("---- WAITING FOR CAR ----                              ");
                }
            }
        }
    }
}