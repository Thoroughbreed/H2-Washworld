using System;
using WashWorldParking.BLL;

namespace WashWorldParking
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo menuKey;
            
            Park myPark = new Park("Parkworld");
            Wash myWash = new Wash("Waterworld");
            do
            {
                Menu(myPark.ParkName, myWash.WashName);
                menuKey = Console.ReadKey(true);
                switch (menuKey.Key)
                {
                    case ConsoleKey.W:
                        Console.WriteLine("W");
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine("WO");
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine("WOS");
                        break;
                    case ConsoleKey.H:
                        Console.WriteLine("WOSH");
                        break;
                    case ConsoleKey.P:
                        break;
                    case ConsoleKey.A:
                        break;
                    case ConsoleKey.R:
                        break;
                    case ConsoleKey.C:
                        break;
                    default:
                        break;
                }
            } while (menuKey.Key != ConsoleKey.X);
        }

        static void Menu(string pname, string wname)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════╗");
            Console.WriteLine("║  {0} | {1}  ║", wname, pname);
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