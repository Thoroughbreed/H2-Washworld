using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.BLL;

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

        public WashTypes(int num)
        {
            WashID = num;
            Busy = false;
        }

        public void WashBronze(CancellationToken _)
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

        public void WashSilver(CancellationToken _)
        {
            Busy = true;

            FriendlyName = "Silver wash";
            FriendlyName = "Washing ...            ";
            Thread.Sleep(5000);
            FriendlyName = "Washing rims ...       ";
            Thread.Sleep(2500);
            FriendlyName = "Washing underside ...  ";
            Thread.Sleep(2500);
            FriendlyName = "Dying ...              ";
            Thread.Sleep(5500);
            WashStatus = "";
            Busy = false;
        }

        public void WashGold(CancellationToken _)
        {
            _.ThrowIfCancellationRequested();
            Busy = true;
            FriendlyName = "Washing ...            ";
            _.ThrowIfCancellationRequested();
            Thread.Sleep(5000);
            FriendlyName = "Washing rims ...       ";
            _.ThrowIfCancellationRequested();
            Thread.Sleep(2500);
            FriendlyName = "Washing underside ...  ";
            _.ThrowIfCancellationRequested();
            Thread.Sleep(2500);
            FriendlyName = "Dying ...              ";
            _.ThrowIfCancellationRequested();
            Thread.Sleep(4500);
            FriendlyName = "Polishing ...          ";
            _.ThrowIfCancellationRequested();
            Thread.Sleep(10000);
            FriendlyName = "Extra dry martini!     ";
            _.ThrowIfCancellationRequested();
            Thread.Sleep(4000);
            
            Busy = false;
        }

        private decimal GeneratePrice(bool member, int type)
        {
            if (member) return 0;
            if (type == 3) return 195;
            if (type == 2) return 125;
            if (type == 1) return 75;
            else return 999;
        }

        public decimal WashNow(int type, bool member, CancellationTokenSource HALT)
        {
            CancellationToken _ = HALT.Token;
            switch(type)
            { 
                case 1:
                    W = new Task(() => WashBronze(_));
                    break;
                case 2:
                    W = new Task(() => WashSilver(_));
                    break;
                case 3:
                    W = new Task(() => WashGold(_));
                    break;
            }
            return GeneratePrice(member, type);
        }
    }
}