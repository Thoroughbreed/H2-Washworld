using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.BLL;
using WashWorldParking.UTIL;

namespace WashWorldParking.REPO
{
    public class WashTypes
    {
        public string FriendlyName { get; set; }
        public decimal Price { get; set; }
        public string WashStatus { get; set; }
        public int WashID { get; set; }
        public bool Busy { get; set; }
        public Task W { get; set; }
        private int x;

        public WashTypes(int num)
        {
            WashID = num;
            Busy = false;
        }

        public void WashBronze()
        {
            Busy = true;

            FriendlyName = "Bronze wash";
            WashStatus = "Washing ...";
            Thread.Sleep(5000);
            WashStatus = "Drying ...";
            Thread.Sleep(4500);
            WashStatus = "";
            Busy = false;
        }

        public void WashSilver()
        {
            Busy = true;

            FriendlyName = "Silver wash";
            WashStatus = "Washing ...            ";
            Thread.Sleep(5000);
            WashStatus = "Washing rims ...       ";
            Thread.Sleep(2500);
            WashStatus = "Washing underside ...  ";
            Thread.Sleep(2500);
            WashStatus = "Dying ...              ";
            Thread.Sleep(5500);
            WashStatus = "";
            Busy = false;
        }

        public void WashGold(BackgroundWorker worker)
        {
            Busy = true;
            x = 1;
            while (!worker.CancellationPending)
            {
                FriendlyName = "Golden shower";
                if (x == 1)
                {
                    WashStatus = "Washing ...            ";
                    Thread.Sleep(5000);
                }
                if (x == 2)
                {
                    WashStatus = "Washing rims ...       ";
                    Thread.Sleep(2500);
                }
                if (x == 3)
                {
                    WashStatus = "Washing underside ...  ";
                    Thread.Sleep(2500);
                }
                if (x == 4)
                {
                    WashStatus = "Dying ...              ";
                    Thread.Sleep(4500);
                }
                if (x == 5)
                {
                    WashStatus = "Polishing ...          ";
                    Thread.Sleep(10000);
                }
                if (x == 6)
                {
                    WashStatus = "Extra dry martini!     ";
                    Thread.Sleep(4000);
                    break;
                }
                x++;
            }
            if (worker.CancellationPending)
            {
                WashStatus = "Emergency halted";
                Busy = false;
                Halt(FriendlyName);
            }
            else WashStatus = "";
            Busy = false;
        }

        private void Halt(string t)
        {
            System.Console.WriteLine("!!!! WASH IS STOPPED !!!!");
            System.Console.WriteLine(t + " is stopped now.");
            throw new StopWash();
        }

        private decimal GeneratePrice(bool member, int type)
        {
            if (member) return 0;
            if (type == 3) return 195;
            if (type == 2) return 125;
            if (type == 1) return 75;
            else return 999;
        }

        public decimal WashNow(int type, bool member, BackgroundWorker worker, DoWorkEventArgs e)
        {
            switch(type)
            { 
                case 1:
                    W = new Task(() => WashBronze());
                    break;
                case 2:
                    W = new Task(() => WashSilver());
                    break;
                case 3:
                    W = new Task(() => WashGold(worker));
                    break;
            }
            return GeneratePrice(member, type);
        }
    }
}