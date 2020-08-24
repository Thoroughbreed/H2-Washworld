using System;
using System.IO;

namespace WashWorldParking.UTIL
{
    static class FileLogger
    {
        static string fileName = "logs.prk";

        public static void WriteToLog(string logmessage)
        {
            string writeMsg = DateTime.Now.ToString() + " " + logmessage + "\n";
            File.AppendAllText(fileName, writeMsg);
        }

        public static string ReadFromLog()
        {
            DirectoryInfo findLog = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo filer in findLog.GetFiles())
            {
                if (filer.Extension == ".prk")
                {
                    return File.ReadAllText(filer.FullName);
                }
            }
            return "Der er ikke fundet nogen log.";
        }
    }
}