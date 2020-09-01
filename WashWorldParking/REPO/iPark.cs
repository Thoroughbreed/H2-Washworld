using System;
using System.Collections.Generic;
using WashWorldParking.MDL;

namespace WashWorldParking.REPO
{
    public interface iPark
    {
        public string ParkName { get; }
        public List<ParkTypes> Parkings { get; set; }

        public int ParkCar(ConsoleKeyInfo input, string lPlate);
        public string AddParkTime(string lPlate, int hours);
        public string RevokeTicket(string lPlate);
        public decimal CheckoutParking(string lPlate);
    }
}
