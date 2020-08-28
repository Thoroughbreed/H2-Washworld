using System;
using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.BLL;
using WashWorldParking.MDL;
using WashWorldParking.REPO;
using WashWorldParking.UTIL;

namespace WashWorldParking
{
    class Program
    {
        public static Park myPark { get; set; }
        public static Wash myWash { get; set; }

        static void Main(string[] args)
        {
            ConsoleKeyInfo menuKey;
            ConsoleKeyInfo subMenuKey;
            int q = 0;

            string lPlate;
            decimal fee;
            bool isAdmin = false;

            Console.Clear();
            Task loadPark = Task.Factory.StartNew(() => LoadPark("ParkWorld"));
            Task loadWash = Task.Factory.StartNew(() => LoadWash("WaterWorld"));
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("˙ǝsɐǝןd punoɹɐ uǝǝɹɔs uɹnʇ\nuǝɥʇ 'sıɥʇ pɐǝɹ uɐɔ noʎ ɟı");
            while (!loadPark.IsCompleted || !loadWash.IsCompleted)
            {
                Console.SetCursorPosition(q, 2);
                Console.WriteLine(".");
                q++;
                Thread.Sleep(250);
            }
            do
            {
                if (args.Length > 0 && args[0] == "-admin") isAdmin = ASCII.AdminMenu();
                else Menu(myPark.ParkName, myWash.WashName);
                menuKey = Console.ReadKey(true);
                switch (menuKey.Key)
                {
                #region Vaskedelen af menuen
                    case ConsoleKey.W:
                        #region Wash
                        ConsoleKeyInfo subMenu;

                        Console.WriteLine("Trying to read license plate");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(100);
                        }
                        Console.WriteLine();
                        do
                        {
                            Console.WriteLine("Please input license plate manually:");
                            lPlate = Console.ReadLine();
                            Console.Clear();
                            if (myWash.CheckLicenseplate(lPlate))
                            {
                                decimal[] _ = new decimal[2];
                                try
                                {
                                    _ = myWash.StartWash(myWash.GetMemberWashType(lPlate), true);
                                    Console.WriteLine("Please enter washbooth number " + _[0]);
                                    MenuWait();
                                    break;
                                }
                                catch (NoWash ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    subMenu = MenuExit();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please select washtype:");
                                Console.WriteLine("[1] Bronze\n[2] Silver\n[3] Gold");
                                try
                                {
                                    int washSelect = Convert.ToInt16(Console.ReadLine());
                                    decimal[] _ = new decimal[2];
                                    _ = myWash.StartWash(washSelect, false);
                                    Console.WriteLine("Please enter washbooth number " + _[0]);
                                    Console.WriteLine("You will be deducted {0:C} from your creditcard", _[1]);
                                    MenuWait();
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("That wasn't a number, now was it?!");
                                    subMenu = MenuExit();
                                }
                                catch (NoWash ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    subMenu = MenuExit();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Something happened??");
                                    Console.WriteLine(ex.Message);
                                    subMenu = MenuExit();
                                }
                            };
                        }
                        while(subMenu.Key != ConsoleKey.X);
                        break;
                    #endregion
                    case ConsoleKey.O:
                        #region Open account
                        Console.WriteLine("Please fill out this form:");
                        Console.WriteLine();
                        Console.Write("Please input license plate: ");
                          lPlate = Console.ReadLine();
                        Console.Write("Please input your creditcard number: ");
                          string cCard = Console.ReadLine();
                        Console.Write("Please input your e-mail: ");
                          string eMail = Console.ReadLine();
                        Console.WriteLine("What type of wash you'd like to subscribe for:");
                        Console.WriteLine("[1] - Bronze wash (120DKK/Month)");
                        Console.WriteLine("[2] - Silver wash (150DKK/Month)");
                        Console.WriteLine("[3] - Golden shower (199DKK/Month)");
                        Console.Write("Please select: ");
                          string selection = Console.ReadLine();
                        while (selection.Length > 1)
                        {
                            Console.WriteLine("Please try again.");
                            Console.WriteLine("What type of wash you'd like to subscribe for:");
                            Console.WriteLine("[1] - Bronze wash (120DKK/Month)");
                            Console.WriteLine("[2] - Silver wash (150DKK/Month)");
                            Console.WriteLine("[3] - Golden shower (199DKK/Month)");
                            Console.Write("Please select: ");
                            selection = Console.ReadLine();
                        }
                        try
                        {
                            int wType = Convert.ToInt16(selection);
                            if (wType > 3)
                            {
                                throw new OutOfRange();
                            }
                            Console.Clear();
                            bool check = myWash.CreateAccount(lPlate, cCard, eMail, wType);
                            if (check) Console.WriteLine("You have created an account for {0}", lPlate);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Whoopsie, it looks like you tried to\ninput something that isn't a number.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something happened");
                            Console.WriteLine(ex.Message);
                        }
                        MenuWait();
                        break;
                    #endregion
                    case ConsoleKey.S:
                        #region See account
                        WashMembers x = null;
                        Console.Write("Please input your license plate: ");
                        lPlate = Console.ReadLine();
                        if (!myWash.CheckLicenseplate(lPlate))
                        {
                            Console.WriteLine("I'm sorry Dave. I cannot let you do that ...");
                            MenuWait();
                            break;
                        }
                        foreach (var item in myWash.Members)
                        {
                            if (item.LPlate == lPlate)
                            {
                                x = item;
                                Console.WriteLine("Welcome " + item.LPlate);
                                Console.WriteLine("You are currently subscribed to: " + item.WashName);
                                break;
                            }
                        }
                        Console.WriteLine("Do you want to change subscription? [Y/N]");
                        ConsoleKeyInfo answer = Console.ReadKey(true);
                        switch (answer.Key)
                        {
                            case ConsoleKey.Y:
                                Console.WriteLine("Choose one of the following:");
                                Console.WriteLine("[1] - Bronze wash");
                                Console.WriteLine("[2] - Silver wash");
                                Console.WriteLine("[3] - Gold wash");
                                Console.WriteLine("[C] - Cancel subscription");
                                ConsoleKeyInfo a2 = Console.ReadKey(true);
                                switch (a2.Key)
                                {
                                    case ConsoleKey.D1:
                                        x.WashType = 1;
                                        x.WashName = "Bronze wash";
                                        Console.WriteLine("You canged to " + x.WashName);
                                        break;
                                    case ConsoleKey.D2:
                                        x.WashType = 2;
                                        x.WashName = "Silver wash";
                                        Console.WriteLine("You canged to " + x.WashName);
                                        break;
                                    case ConsoleKey.D3:
                                        x.WashType = 3;
                                        x.WashName = "Golden shower";
                                        Console.WriteLine("You canged to " + x.WashName);
                                        break;
                                    case ConsoleKey.C:
                                        Console.WriteLine("Are you sure you want to cancel subscription? [Y/N]");
                                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                        {
                                            myWash.Members.Remove(x);
                                            Console.WriteLine("K thx bai!");
                                        }
                                        else Console.WriteLine("Phew! You're still here then!");
                                        break;
                                }
                                break;
                            case ConsoleKey.N:
                                break;
                            default:
                                Console.WriteLine("You did something wrong.");
                                MenuWait();
                                break;
                        }

                        MenuWait();
                        break;
                        #endregion
                    case ConsoleKey.H:
                        Console.WriteLine("Are you sure that you want to emergency halt all the washers?");
                        //Task.Factory.StartNew(() => HorisontalWash(0, 20));
                        Console.WriteLine("Holla the status!");
                        myWash.StatusText();
                        myWash.HALT.Cancel();
                        Console.SetCursorPosition(0, 17);
                        MenuWait();
                        break;
                #endregion

                #region Parkeringsdelen af menuen
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
                                    Console.WriteLine("License plate: {0} | Parked since: {1} | Parking type: {2}", item.LicensePlate, item.ParkTime, item.FriendlyName);
                                    occ++;
                                }
                            }
                            Console.WriteLine("Number of occupied spaces: " + occ);
                            Console.WriteLine("Number of available spaces: " + (myPark.Parkings.Count - occ));
                            Console.WriteLine();
                            foreach (var item in myWash.Members)
                            {
                                Console.WriteLine("License plate: {0} | Type of subscription: {1}", item.LPlate, item.WashName);
                            }
                            Console.WriteLine("Total number of subscribers: " + myWash.Members.Count);
                        }
                        MenuWait();
                        
                        break;
                    case ConsoleKey.R:
                        Console.Write("Please enter your license plate: ");
                        lPlate = Console.ReadLine();
                        try
                        {
                            myPark.RevokeTicket(lPlate);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("It doesn't look like that was a correct license plate.\nWould you like to try again?");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("It doesn't look like the license plate {0} is parked at the moment.", lPlate);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
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
                #endregion

                #region Super secret admin area!

                #endregion
                }
            } while (menuKey.Key != ConsoleKey.X);
            var closeP = Task.Factory.StartNew (() => FileLogger.SavePark(myPark.Parkings));
            var closeW = Task.Factory.StartNew(() => FileLogger.SaveWash(myWash.Members));
            Console.WriteLine("Saving park and wash");
            var spinner = Task.Factory.StartNew(() => ASCII.Spinner(21,16));
            closeP.Wait();
            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Saving park completed!");
            closeW.Wait();
            Console.SetCursorPosition(0, 18);
            Console.WriteLine("Saving wash completed!");
            Console.SetCursorPosition(21, 16);
            Console.Write(" ");
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("K thx bai!");
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

        static void MenuWait()
        {
            Console.WriteLine("Press any key to continue");
            Console.SetCursorPosition(30, 2);
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine();
            }
            Console.ReadKey(true);
        }

        static ConsoleKeyInfo MenuExit()
        {
            Console.WriteLine("Press any key to try again");
            Console.WriteLine("Press 'X' to exit");
            ConsoleKeyInfo _ = Console.ReadKey(true);
            return _;
        }

        static void LoadPark(string pName)
        {
            myPark = new Park(pName);
        }

        static void LoadWash(string wName)
        {
            myWash = new Wash(wName);
        }

    }
}