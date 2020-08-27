namespace WashWorldParking.REPO
{
    public class WashTypes
    {
        public string FriendlyName { get; set; }
        public double Price { get; set; }
        public bool IsMember { get; set; }
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

        public void WashBronze(bool member)
        {
            Busy = true;

            FriendlyName = "Bronze wash";
            Price = 75;
            IsMember = member;
            Wash = true;
            Dry = true;
            RimWash = false;
            Underside = false;
            Polish = false;
            ExtraDry = false;
        }

        public void WashSilver(bool member)
        {
            Busy = true;

            FriendlyName = "Silver wash";
            Price = 125;
            IsMember = member;
            Wash = true;
            Dry = true;
            RimWash = true;
            Underside = true;
            Polish = false;
            ExtraDry = false;
        }

        public void WashGold(bool member)
        {
            Busy = true;

            FriendlyName = "Gold wash";
            Price = 195;
            IsMember = member;
            Wash = true;
            Dry = true;
            RimWash = true;
            Underside = true;
            Polish = true;
            ExtraDry = true;
        }

        public bool WashNow()
        {
            return false;
        }
    }
}