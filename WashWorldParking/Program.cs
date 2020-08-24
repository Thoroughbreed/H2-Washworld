using System;

namespace WashWorldParking
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
            UTIL.FileLogger.WriteToLog("Tissemand");
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║  Waterworld | Parkworld  ║");
            Console.WriteLine("╠══════════════════════════╣");
            Console.WriteLine("║      Please select:      ║");
            Console.WriteLine("║      [W]ash car          ║");
            Console.WriteLine("║      [O]pen account      ║");
            Console.WriteLine("║      [S]ee account       ║");
            Console.WriteLine("║      [H]eureka!          ║");
            Console.WriteLine("║                          ║");
            Console.WriteLine("║      [P]ark              ║");
            Console.WriteLine("║      [A]add time         ║");
            Console.WriteLine("║      [R]evoke ticket     ║");
            Console.WriteLine("║      [C]ancel parking    ║");
            Console.WriteLine("║                          ║");
            Console.WriteLine("║      [X] Exit            ║");
            Console.WriteLine("╚══════════════════════════╝");
        }
    }
}