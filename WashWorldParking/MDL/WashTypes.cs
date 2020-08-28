using System.Threading;
using System.Threading.Tasks;

namespace WashWorldParking.REPO
{
    public class WashTypes
    {
        public string FriendlyName { get; set; }
        public decimal Price { get; set; }
        public bool Wash { get; set; }
        public bool Dry { get; set; }
        public bool RimWash { get; set; }
        public bool Underside { get; set; }
        public bool Polish { get; set; } // Not the country!
        public bool ExtraDry { get; set; }
        public int WashID { get; set; }
        public bool Busy { get; set; }

        public WashTypes(int num)
        {
            WashID = num;
            Busy = false;
        }

        public decimal WashBronze(bool member)
        {
            Busy = true;

            FriendlyName = "Bronze wash";
            Price = GeneratePrice(member, 1);
            Wash = true;
            Dry = true;
            RimWash = false;
            Underside = false;
            Polish = false;
            ExtraDry = false;
            Thread.Sleep(10000);
            Busy = false;
            return Price;
        }

        public void WashSilver(bool member)
        {
            Busy = true;

            FriendlyName = "Silver wash";
            Price = GeneratePrice(member, 2);
            Wash = true;
            Dry = true;
            RimWash = true;
            Underside = true;
            Polish = false;
            ExtraDry = false;
            Thread.Sleep(20000);
            Busy = false;
        }

        public void WashGold(bool member)
        {
            Busy = true;

            FriendlyName = "Gold wash";
            Price = GeneratePrice(member, 3);
            Wash = true;
            Dry = true;
            RimWash = true;
            Underside = true;
            Polish = true;
            ExtraDry = true;
            Thread.Sleep(30000);
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

        public decimal WashNow(int type, bool member)
        {
            switch (type)
            {
                case 1:
                    Task.Run(() => WashBronze(member));
                    break;
                case 2:
                    Task.Run(() => WashSilver(member));
                    break;
                case 3:
                    Task.Run(() => WashGold(member));
                    break;
            }
            return GeneratePrice(member, type);
        }
    }
}