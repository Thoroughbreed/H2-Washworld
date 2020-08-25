using System;
using System.Threading;
using WashWorldParking.BLL;

namespace WashWorldParking
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo menuKey;
            string lPlate;
            
            Park myPark = new Park("Parkworld");
            Wash myWash = new Wash("Waterworld");
            do
            {
                Menu(myPark.ParkName, myWash.WashName);
                menuKey = Console.ReadKey(true);
                switch (menuKey.Key)
                {
                    case ConsoleKey.W:
                        Console.WriteLine("Trying to read license plate");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(100);
                        }
                        Console.WriteLine("Failed. Please input license plate manually:");
                          lPlate = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine(myWash.WashCar(lPlate));
                        MenuWait();
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine("Please fill out this form:");
                        Console.WriteLine();
                        Console.Write("Please input license plate: ");
                          lPlate = Console.ReadLine();
                        Console.Write("Please input your creditcard number: ");
                          string cCard = Console.ReadLine();
                        Console.Write("Please input your e-mail: ");
                          string eMail = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine(myWash.CreateAccount(lPlate, cCard, eMail));
                        MenuWait();
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine("WOS");
                        MenuWait();
                        break;
                    case ConsoleKey.H:
                        Console.WriteLine("WOSH");
                        MenuWait();
                        break;
                    case ConsoleKey.P:
                        Console.WriteLine("Please input your vehicle type:");
                        Console.WriteLine("1 - Car");
                        Console.WriteLine("2 - Car - Handicap parking viable");
                        Console.WriteLine("3 - Truck");
                        Console.WriteLine("4 - Bus");
                        Console.Write("Input: ");
                        ConsoleKeyInfo pType = Console.ReadKey();
                        Console.WriteLine();
                        Console.Write("Please input your license plate: ");
                        lPlate = Console.ReadLine();
                        myPark.ParkCar(pType, lPlate);
                        Console.Clear();
                        Console.WriteLine("Parking started. The time is now: {0}", DateTime.Now.ToString());
                        Console.WriteLine("Minimum parking time is two (2) hours.\nYour parking will expire at: {0}", DateTime.Now.AddHours(2).ToString());
                        MenuWait();
                        break;
                    case ConsoleKey.A:
                        Console.Write("Please input your license plate: ");
                        lPlate = Console.ReadLine();
                        Console.Write("Please input how many hours you want to add: ");
                        string stringTime = Console.ReadLine();
                        int addedTime = 0;
                        try
                        {
                            addedTime = Convert.ToInt16(stringTime);
                            if (addedTime < 1)
                            {
                                throw new FormatException("Only positive numbers allowed!");
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Please only input a number!");
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("You broke the program ...");
                            Console.WriteLine(ex.Message);
                        }
                        myPark.AddParkTime(lPlate, addedTime);
                        MenuWait();
                        break;
                    case ConsoleKey.R:
                        MenuWait();
                        break;
                    case ConsoleKey.C:
                        MenuWait();
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

        static void MenuWait()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }
    }
}