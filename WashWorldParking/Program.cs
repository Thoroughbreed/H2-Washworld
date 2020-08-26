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
            ConsoleKeyInfo subMenuKey;
            string lPlate;
            decimal fee;
            bool isAdmin = false;

            Park myPark = new Park("Parkworld");
            Wash myWash = new Wash("Waterworld");
            do
            {
                if (args.Length >   0 && args[0] == "-admin") isAdmin = AdminMenu();
                else Menu(myPark.ParkName, myWash.WashName);
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
                        Convert.ToInt16(lPlate);
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
                    #region Parker bil - kontrollerer om nummerpladen allerede er parkeret
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
                        int result = myPark.ParkCar(pType, lPlate);
                        if (result == 1) Console.WriteLine("Unfortunately we don't have any free parking spaces for your automobile type.\nPlease try again later.");
                        else if (result == 2) Console.WriteLine("License plate already parked!");
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Parking started. The time is now: {0}", DateTime.Now.ToString());
                            Console.WriteLine("Minimum parking time is two (2) hours.\nYour parking will expire at: {0}", DateTime.Now.AddHours(2).ToString());
                        }
                        MenuWait();
                        break;
                    #endregion
                    case ConsoleKey.A: // Tilføjer tid til parkering | Viser al info som admin
                        if (!isAdmin)
                        {
                            do
                            {
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
                                    Console.WriteLine(myPark.AddParkTime(lPlate, addedTime));
                                    break;
                                }
                                catch (NullReferenceException)
                                {
                                    Console.WriteLine("It doesn't appear that the license plate exists in our system.");
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
                                    Console.WriteLine(ex);
                                }
                                subMenuKey = MenuExit();
                            }
                            while (subMenuKey.Key != ConsoleKey.X);
                        }
                        if (isAdmin)
                        {
                            int occ = 0;
                            foreach (var item in myPark.Parkings)
                            {
                                if (item.Occupied)
                                {
                                    Console.WriteLine("License plate: " + item.LicensePlate + " | Parked since: " + item.ParkTime + " | Parking type: " + item.FriendlyName);
                                    occ++;
                                }
                            }
                            Console.WriteLine("Number of occupied spaces: " + occ);
                            Console.WriteLine("Number of available spaces: " + (myPark.Parkings.Count - occ));
                        }
                        MenuWait();
                        
                        break;
                    case ConsoleKey.R:
                        Console.Write("Please enter your license plate: ");
                        lPlate = Console.ReadLine();
                        try
                        {
                            fee = myPark.RevokeTicket(lPlate);
                            Console.WriteLine(fee);
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("It doesn't look like the license plate {0} is parked at the moment.", lPlate);
                        }
                        MenuWait();
                        break;
                    case ConsoleKey.C:
                        Console.Write("Please enter your license plate: ");
                        lPlate = Console.ReadLine();
                        try
                        {
                            fee = myPark.CheckoutParking(lPlate);
                            Console.WriteLine("Thank you for parking at ParkWorld.\nYour fee is {0:C}, and will be deducted from your creditcard automatically.", fee);
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("It doesn't look like the license plate {0} is parked at the moment.", lPlate);
                        }
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
            Console.WriteLine("║      [A]dd time          ║");
            Console.WriteLine("║      [R]evoke ticket     ║");
            Console.WriteLine("║      [C]heckout parking  ║");
            Console.WriteLine("║                          ║");
            Console.WriteLine("║      [X] Exit            ║");
            Console.WriteLine("╚══════════════════════════╝");
        }

        static bool AdminMenu()
        {
            Console.Clear();
            Console.WriteLine(@"8888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine(@"8888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine(@"8888888888888888888888888P""""  """"98888888888888888888888888888");
            Console.WriteLine(@"8888888888888888P""88888P          988888""98888888888888888888");
            Console.WriteLine(@"8888888888888888  ""9888            888P""  8888888888888888888");
            Console.WriteLine(@"888888888888888888bo ""9  d8o  o8b  P"" od888888888888888888888");
            Console.WriteLine(@"888888888888888888888bob 98""  ""8P dod888888888888888888888888");
            Console.WriteLine(@"888888888888888888888888    db    888888888888888888888888888");
            Console.WriteLine(@"88888888888888888888888888      88888888888888888888888888888");
            Console.WriteLine(@"88888888888888888888888P""9bo  odP""988888888888888888888888888");
            Console.WriteLine(@"88888888888888888888P"" od88888888bo ""988888888888888888888888");
            Console.WriteLine(@"888888888888888888   d88888888888888b   888888888888888888888");
            Console.WriteLine(@"8888888888888888888oo8888888888888888oo8888888888888888888888");
            Console.WriteLine(@"8888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║             WELCOME TO THE SECRET ADMIN MODE!             ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                      Please select:                       ║");
            Console.WriteLine("║     _.........._                         _.........._     ║");
            Console.WriteLine("║    | |  DOOM  | |    [I]nspect          | |  DOOM  | |    ║");
            Console.WriteLine("║    | | DISK 1 | |    [D]issect          | | DISK 2 | |    ║");
            Console.WriteLine("║    | |  OF 4  | |    [K]ill init        | |  OF 4  | |    ║");
            Console.WriteLine("║    | |________| |    [F]ind user        | |________| |    ║");
            Console.WriteLine("║    |   ______   |    [A]ll information  |   ______   |    ║");
            Console.WriteLine("║    |  |    | |  |                       |  |    | |  |    ║");
            Console.WriteLine("║    |__|____|_|__|    [X] Exit           |__|____|_|__|    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            return true;
        }

        static void MenuWait()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }

        static ConsoleKeyInfo MenuExit()
        {
            Console.WriteLine("Press any key to try again");
            Console.WriteLine("Press 'X' to exit");
            ConsoleKeyInfo _ = Console.ReadKey(true);
            return _;
        }
    }
}