using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.MDL;

namespace WashWorldParking.UTIL
{
    static class FileLogger
    {
        static string fileName = "logs.log";
        static string parkName = "file.prk";
        static string washName = "file.wsh";

        public static void WriteToLog(string logmessage)
        {
            string writeMsg = DateTime.Now.ToString() + " " + logmessage + "\n";
            File.AppendAllTextAsync(fileName, writeMsg);
        }

        public static void SavePark(List<ParkTypes> arg)
        {
            string writeMsg = "";
            foreach (var item in arg)
            {
                writeMsg += item.FriendlyName + "" + item.BoxSize + "" + item.Price + "" + item.Occupied + "" + item.ParkTime + "" + item.ExpirationTime + "" + item.BoxID + "" + item.LicensePlate + "\n";
            }
            File.WriteAllTextAsync(parkName, writeMsg);
        }

        public static void SaveWash()
        {
            //TODO gem vaskehallen
        }

        public static string ReadFromLog()
        {
            DirectoryInfo findLog = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo filer in findLog.GetFiles())
            {
                if (filer.Extension == ".log")
                {
                    return File.ReadAllText(filer.FullName);
                }
            }
            return "Der er ikke fundet nogen log.";
        }

        public static List<ParkTypes> ReadFromPark()
        {
            string[] fileInfo;
            List<ParkTypes> _ = new List<ParkTypes>();

            DirectoryInfo findLog = new DirectoryInfo(Environment.CurrentDirectory);
            fileInfo = null;
            foreach (FileInfo filer in findLog.GetFiles())
            {
                if (filer.Extension == ".prk")
                {
                    fileInfo = File.ReadAllLines(filer.FullName);
                }
            }

            foreach (string item in fileInfo)
            {
                string[] split = item.Split('');
                if (split[0] == "Carpark")
                {
                    _.Add(new CarPark(split[0], Convert.ToInt16(split[1]), Convert.ToDecimal(split[2]), Convert.ToBoolean(split[3]), split[4], split[5], Convert.ToInt16(split[6]), split[7]));
                }
                if (split[0] == "Handicap parking")
                {
                    _.Add(new HandiPark(split[0], Convert.ToInt16(split[1]), Convert.ToDecimal(split[2]), Convert.ToBoolean(split[3]), split[4], split[5], Convert.ToInt16(split[6]), split[7]));
                }
                if (split[0] == "Truck stop")
                {
                    _.Add(new TruckPark(split[0], Convert.ToInt16(split[1]), Convert.ToDecimal(split[2]), Convert.ToBoolean(split[3]), split[4], split[5], Convert.ToInt16(split[6]), split[7]));
                }
                if (split[0] == "Bus stop")
                {
                    _.Add(new BusPark(split[0], Convert.ToInt16(split[1]), Convert.ToDecimal(split[2]), Convert.ToBoolean(split[3]), split[4], split[5], Convert.ToInt16(split[6]), split[7]));
                }
            }
            return _;
        }
    }
}