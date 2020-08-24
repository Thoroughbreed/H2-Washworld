using System;
using System.Collections.Generic;
using WashWorldParking.REPO;

namespace WashWorldParking.BLL
{ 
    public class Wash : iWash
    {
        public string WashName { get; }
        public Wash(string name)
        {
            WashName = name;
        }

        public List<WashTypes> GetWashTypes()
        {
            return null;
        }

        public string WashCar(string lPLate)
        {
            return "Welcome to my cadaver";
        }

        public bool CreateAccount(string lPlate, string cCard, string eMail)
        {
            bool result = false;

            return result;
        }
    }
}
