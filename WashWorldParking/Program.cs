using System;
using System.Collections.Generic;
using System.Linq;
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
        public static List<Victims> Victims { get; set; }
        private static ASCII MainMenu;
        private static bool DOOM;

        static void Main(string[] args)
        {
            ConsoleKey menuKey;
            ConsoleKeyInfo subMenuKey;
            int q = 0;
            int z = 1;

            string lPlate;
            decimal fee;
            bool isAdmin = false;
            bool init = true;

            Console.Clear();
            Console.CursorVisible = false;
            Task loadPark = Task.Factory.StartNew(() => LoadPark("ParkWorld"));
            Task loadWash = Task.Factory.StartNew(() => LoadWash("WaterWorld"));
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("˙ǝsɐǝןd punoɹɐ uǝǝɹɔs uɹnʇ\nuǝɥʇ 'sıɥʇ pɐǝɹ uɐɔ noʎ ɟı");
            Thread.Sleep(100);
            while (!loadPark.IsCompleted || !loadWash.IsCompleted)
            {
                Console.SetCursorPosition(q, 2);
                Console.WriteLine(".");
                q++;
                Thread.Sleep(250);
            }
            do
            {
                #region Startup parameter checker
                if (args.Length > 0) // Checking for startup parameters
                {
                    if ((args[0] == "-admin") || (args[0] == "-iddqd")) 
                    {
                        isAdmin = ASCII.AdminMenu(); // Loads admin menu if parameter is admin or iddqd
                        if (init)
                        {
                            FileLogger.WriteToLog($"Application started as {args[0]}"); init = false; // set initial startup as false
                            Victims = new List<Victims>();
                            foreach (var item in myWash.Members)
                            {
                                Victims.Add(new Victims(item.LPlate, z, "Wash"));
                                z++;
                            }
                            foreach (var item in myPark.Parkings)
                            {
                                if (item.Occupied) { Victims.Add(new Victims(item.LicensePlate, z, "Park")); z++; };
                            }
                        };
                    }
                    else
                    {
                        // Loads "fancy" menu for normal users if wrong parameters are entered
                        MainMenu = new ASCII(new List<string>()
                           { "Wash car", "Create Account", "See account", "See wash status", "Park car", "Add time", "Revoke ticket", "Checkout parking", "-- EXIT --"});
                        if (init) { FileLogger.WriteToLog($"Application was started with (wrong) parameter ({args[0]})"); init = false; };
                    }
                }
                #endregion
                else
                {
                    // Loads "fancy" menu for normal users
                    MainMenu = new ASCII(new List<string>()
                      { "Wash car", "Create Account", "See account", "See wash status", "Park car", "Add time", "Revoke ticket", "Checkout parking", "-- EXIT --" });
                }
                try
                {
                    if (!isAdmin) menuKey = MainMenu.Draw();
                    else menuKey = Console.ReadKey(true).Key;
                }
                catch (DOOM ex)
                {
                    // Catches the "DOOM" exception/easter egg
                    Console.WriteLine(ex.Message);
                    ASCII.DOOM();
                    DOOM = true;
                    break;
                }
                switch (menuKey)
                {
                #region Vaskedelen af menuen
                    case ConsoleKey.W:
                        #region Wash
                        // Washes a car (cars only! No trucks or busses!)
                        // Fake license plate scanner :)
                        Console.WriteLine("Trying to read license plate");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(50);
                        }
                        Console.WriteLine();
                        do
                        {
                            try
                            {
                                // Checks for available wash - throws exception if none found.
                                WashTypes _ = myWash.Washes.Find(s => s.Busy == false);
                                if (_ == null) throw new NoWash();
                                Console.WriteLine("Please input license plate manually:");
                                lPlate = Console.ReadLine();
                                FileLogger.WriteToLog($"License plate {lPlate} was entered in the system.");
                                Console.Clear();
                                // Checks for membership (and what type)
                                if (myWash.CheckLicenseplate(lPlate))
                                {
                                    List<object> arguments = new List<object>();
                                    arguments.Add(myWash.GetMemberWashType(lPlate));
                                    arguments.Add(true);
                                    FileLogger.WriteToLog($"... and it was a member who is subscribed for {Enum.GetName(typeof(WashEnum), ((int)arguments[0]-1))}");

                                    myWash.Worker.RunWorkerAsync(arguments);
                                    _ = myWash.Washes.Find(s => s.Busy == false);
                                    if (_ == null) throw new NoWash();
                                    Console.WriteLine($"Please enter washbooth number {_.WashID}");
                                    FileLogger.WriteToLog($"... and he entered washbooth number {_.WashID}");
                                    MenuWait();
                                    break;
                                }
                                else
                                {
                                    // Non subscribed customer
                                    Console.WriteLine("Please select washtype:");
                                    Console.WriteLine("[1] Bronze\n[2] Silver\n[3] Gold");
                                        int washSelect = Convert.ToInt16(Console.ReadLine());
                                        int price = 0;
                                        Console.WriteLine($"Please enter washbooth number {_.WashID}");
                                    FileLogger.WriteToLog($"... and it was NOT a member - customer selected {Enum.GetName(typeof(WashEnum), (washSelect - 1))}");
                                    FileLogger.WriteToLog($"... and entered washbooth number {_.WashID}");
                                    List<object> arguments = new List<object>();
                                        arguments.Add(washSelect);
                                        arguments.Add(false);

                                        myWash.Worker.RunWorkerAsync(arguments);

                                        if (washSelect == 1) price = 75;
                                        if (washSelect == 2) price = 125;
                                        if (washSelect == 3) price = 195;
                                        Console.WriteLine($"You will be deducted {price:C} from your creditcard");

                                        MenuWait();
                                        break;
                                };
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("That wasn't a number, now was it?!");
                                subMenuKey = MenuExit();
                            }
                            catch (NoWash ex)
                            {
                                Console.WriteLine(ex.Message);
                                FileLogger.WriteToLog(ex.Message);
                                subMenuKey = MenuExit();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Something happened??");
                                Console.WriteLine(ex.Message);
                                FileLogger.WriteToLog(ex.Message);
                                subMenuKey = MenuExit();
                            }
                        }
                        while(subMenuKey.Key != ConsoleKey.X);
                        break;
                        #endregion
                    case ConsoleKey.O:
                        #region Open account
                        // Opens a new wash-subscription account, no regex on creditcard/e-mail
                        do
                        {
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
                            // Checks for empty fields in the form
                            while ((selection.Length == 0) || (selection.Length == 0) || (cCard.Length == 0) || (lPlate.Length == 0) || (eMail.Length == 0))
                            {
                                Console.WriteLine("Please try again.");
                                if (lPlate.Length < 1)
                                {
                                    Console.Write("Please input license plate: ");
                                    lPlate = Console.ReadLine();
                                }
                                if (cCard.Length < 1)
                                {
                                    Console.Write("Please input your creditcard number: ");
                                    cCard = Console.ReadLine();
                                }
                                if (eMail.Length < 1)
                                {
                                    Console.Write("Please input your e-mail: ");
                                    eMail = Console.ReadLine();
                                }
                                if ((selection != "1") || (selection != "2") || (selection != "3"))
                                {
                                    Console.WriteLine("What type of wash you'd like to subscribe for:");
                                    Console.WriteLine("[1] - Bronze wash (120DKK/Month)");
                                    Console.WriteLine("[2] - Silver wash (150DKK/Month)");
                                    Console.WriteLine("[3] - Golden shower (199DKK/Month)");
                                    Console.Write("Please select: ");
                                    selection = Console.ReadLine();
                                }
                            }
                            try
                            {
                                int wType = Convert.ToInt16(selection);
                                if (wType > 3)
                                {
                                    throw new OutOfRange();
                                }
                                FileLogger.WriteToLog($"We have a new client! {lPlate} wants to subscribe for {Enum.GetName(typeof(WashEnum), (wType - 1))}");
                                Console.Clear();
                                bool check = myWash.CreateAccount(lPlate, cCard, eMail, wType);
                                if (check) Console.WriteLine($"You have created an account for {lPlate}");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Whoopsie, it looks like you tried to\ninput something that isn't a number.");
                            }
                            catch (OutOfRange ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Something happened");
                                Console.WriteLine(ex.Message);
                                FileLogger.WriteToLog(ex.Message);
                            }
                            subMenuKey = MenuExit();
                        } while (subMenuKey.Key != ConsoleKey.X);
                        break;
                        #endregion
                    case ConsoleKey.S:
                        #region See account
                        // Lets the user check the user account, and change the wash-type (or cancel)
                        WashMembers x = null;
                        Console.Write("Please input your license plate: ");
                        lPlate = Console.ReadLine();
                        // Checks if plate is subscribed
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
                                Console.WriteLine($"Welcome {item.LPlate}");
                                Console.WriteLine($"You are currently subscribed to: {item.WashName}");
                                break;
                            }
                        }
                        Console.WriteLine("Do you want to change subscription? [Y/N]");
                        ConsoleKeyInfo answer = Console.ReadKey(true);
                        switch (answer.Key)
                        {
                            case ConsoleKey.Y:
                                FileLogger.WriteToLog($"Customer with the license plate {x.LPlate} wants to change their subscription");
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
                                        FileLogger.WriteToLog($"... and he changed to {x.WashName}");
                                        break;
                                    case ConsoleKey.D2:
                                        x.WashType = 2;
                                        x.WashName = "Silver wash";
                                        Console.WriteLine("You canged to " + x.WashName);
                                        FileLogger.WriteToLog($"... and he changed to {x.WashName}");
                                        break;
                                    case ConsoleKey.D3:
                                        x.WashType = 3;
                                        x.WashName = "Golden shower";
                                        Console.WriteLine("You canged to " + x.WashName);
                                        FileLogger.WriteToLog($"... and he changed to {x.WashName}");
                                        break;
                                    case ConsoleKey.C:
                                        Console.WriteLine("Are you sure you want to cancel subscription? [Y/N]");
                                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                        {
                                            FileLogger.WriteToLog($"{x.LPlate} have left the building!... What a tit");
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
                                break;
                        }
                        MenuWait();
                        break;
                        #endregion
                    case ConsoleKey.H:
                        #region Status for wash + ability to emergency halt
                        var __cts = new CancellationTokenSource();
                        CancellationToken __cancellation = __cts.Token;
                        Console.WriteLine("-- WASH STATUS --");
                        Console.WriteLine("Do you want to emergency halt all the washers?");
                        Console.WriteLine("[Y]/[N] - any other key to abort from menu.");
                        // Starts the wash-status update thing async
                        Task stat = Task.Run(() =>
                        {
                            myWash.StatusText(__cancellation);
                        }, __cancellation);
                        Console.SetCursorPosition(0, 17);
                        subMenuKey = Console.ReadKey(true);
                        // Cancels the wash-thread and the status update-thread
                        if (subMenuKey.Key == ConsoleKey.Y)
                        {
                            myWash.Worker.CancelAsync();
                            Thread.Sleep(1000); // Waits a second before proceeding to next cancellation step
                            __cts.Cancel();
                            FileLogger.WriteToLog($"Someone pressed the HALT! button real hard ...");
                        }
                        else // Cancels the status update-thread when exit from menu
                        {
                            __cts.Cancel();
                        }
                        __cts.Dispose();
                        MenuWait();
                        break;
                        #endregion
                #endregion

                #region Parkeringsdelen af menuen
                    case ConsoleKey.P:
                        #region Parks vehicle
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
                        // Checks licenseplate (and type)
                        int result = myPark.ParkCar(pType, lPlate);
                        if (result == 1) Console.WriteLine("Unfortunately we don't have any free parking spaces for your automobile type.\nPlease try again later.");
                        else if (result == 2) Console.WriteLine("License plate already parked!");
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Parking started. The time is now: {0}", DateTime.Now.ToString());
                            Console.WriteLine("Minimum parking time is two (2) hours.\nYour parking will expire at: {0}", DateTime.Now.AddHours(2).ToString());
                            FileLogger.WriteToLog(string.Format("{0} have JUST parked his car! It will expire at {1}", lPlate, DateTime.Now.AddHours(2).ToString()));
                        }
                        MenuWait();
                        break;
                        #endregion
                    case ConsoleKey.A:
                        #region Adds time to parking || Displays all information (non admin/admin)
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
                                    string _ = myPark.AddParkTime(lPlate, addedTime);
                                    Console.WriteLine(_);
                                    FileLogger.WriteToLog($"{lPlate} just added {addedTime} to his parking.");
                                    FileLogger.WriteToLog(_);

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
                                    FileLogger.WriteToLog(ex.Message);
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
                        #endregion
                    case ConsoleKey.R:
                        #region Revoke ticket
                        // Gives the user the ability to refund some of his parking ticket (if viable)
                        Console.Write("Please enter your license plate: ");
                        lPlate = Console.ReadLine();
                        try
                        {
                            string _ = myPark.RevokeTicket(lPlate);
                            FileLogger.WriteToLog($"{lPlate} wants to revoke some of his parking time ...");
                            FileLogger.WriteToLog(_);
                            Console.WriteLine(_);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("It doesn't look like that was a correct license plate.\nWould you like to try again?");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine($"It doesn't look like the license plate {lPlate} is parked at the moment.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            FileLogger.WriteToLog(e.Message);
                        }
                        MenuWait();
                        break;
                        #endregion
                    case ConsoleKey.C:
                        #region Checkout parking
                        // Checks out from parkinglot (if plate is parked)
                        Console.Write("Please enter your license plate: ");
                        lPlate = Console.ReadLine();
                        try
                        {
                            fee = myPark.CheckoutParking(lPlate);
                            Console.WriteLine($"Thank you for parking at ParkWorld.\nYour fee is {fee:C}, and will be deducted from your creditcard automatically.");
                            FileLogger.WriteToLog($"{lPlate} just checked out, and was charged {fee:C} on his creditcard");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine($"It doesn't look like the license plate {lPlate} is parked at the moment.");
                        }
                        MenuWait();
                        break;
                        #endregion
                    default:
                        break;
                #endregion

                #region Super secret admin area!
                    case ConsoleKey.I:
                        // Inspects log file
                        Console.WriteLine(FileLogger.ReadFromLog());
                        MenuWait();
                        break;
                    case ConsoleKey.D:
                        #region Update (dissect?) user
                        // Updates user parameters (including things as parking price)
                        // Checks for wash-user or parking-user
                        Console.WriteLine("Please select the victim from the list below (X to quit!)");
                        foreach (var item in Victims)
                        {
                            Console.WriteLine($"ID: [{item.index}]\tLicense plate: {item.lPlate}\tType: {item.TypeOf}");
                        }
                        Console.Write("Now, which ID do you want to IDDQD: ");
                        string idString = Console.ReadLine();
                        if (idString == "X") break;
                        try
                        {
                            int idInt = Convert.ToInt16(idString);
                            Victims _u = Victims.Find(s => s.index == idInt);
                            WashMembers W = myWash.Members.Find(s => s.LPlate == _u.lPlate);
                            ParkTypes P = myPark.Parkings.Find(s => s.LicensePlate == _u.lPlate);
                            Console.WriteLine("");
                            Console.WriteLine($"You have selected {_u.lPlate}");
                            if (_u.TypeOf == "Wash")
                            {
                                Console.WriteLine(W.WashName);
                                Console.WriteLine(W.CCard);
                                Console.WriteLine(W.EMail);
                                Console.WriteLine("");
                            }
                            else if (_u.TypeOf == "Park")
                            {
                                Console.WriteLine(P.ParkTime);
                                Console.WriteLine(P.ExpirationTime);
                                Console.WriteLine(P.Price);
                                Console.WriteLine("");
                            }
                            Console.WriteLine("[1] Update user");
                            Console.WriteLine("[X] Quitter ...");
                            ConsoleKeyInfo K = Console.ReadKey();
                            Console.WriteLine();
                            switch (K.Key)
                            {
                                case ConsoleKey.D1:
                                    if (_u.TypeOf == "Wash")
                                    {
                                        Console.WriteLine("Choose one of the following:");
                                        Console.WriteLine("[1] - Bronze wash");
                                        Console.WriteLine("[2] - Silver wash");
                                        Console.WriteLine("[3] - Gold wash");
                                        ConsoleKeyInfo a2 = Console.ReadKey(true);
                                        Console.WriteLine();
                                        Console.WriteLine("Now, whatabout his creditcard and e-mail?");
                                        Console.WriteLine("Please enter new creditcard information (leave blank for default): ");
                                        string CC = Console.ReadLine();
                                        if (CC.Length == 0) CC = W.CCard;
                                        Console.WriteLine("Please enter a new e-mail address (leave blank for default): ");
                                        string EM = Console.ReadLine();
                                        if (EM.Length == 0) EM = W.EMail;
                                        Console.WriteLine("Now, should we change the license plate as well?");
                                        string LP = Console.ReadLine();
                                        if (LP.Length == 0) LP = W.LPlate;

                                        Console.WriteLine(myWash.AdminUpd(W, a2, CC, EM, LP));
                                    }
                                    if (_u.TypeOf == "Park")
                                    {
                                        Console.WriteLine("Enter new parktime (leave blank for default): ");
                                        string PT = Console.ReadLine();
                                        if (PT.Length == 0) PT = P.ParkTime;
                                        Console.WriteLine("Enter new expiration time (leave blank for default): ");
                                        string ET = Console.ReadLine();
                                        if (ET.Length == 0) ET = P.ExpirationTime;
                                        Console.WriteLine("Enter new price (leave blank for default): ");
                                        string PR = Console.ReadLine();
                                        decimal Pr = P.Price;
                                        if (PR.Length > 0)
                                        {
                                            try
                                            {
                                                Pr = Convert.ToDecimal(PR);
                                            }
                                            catch (FormatException)
                                            {
                                                throw new BadUser();
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        myPark.AdminUpd(_u.lPlate, PT, ET, Pr);
                                    }
                                    Console.WriteLine("The poor lad was updated ...");
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (FormatException)
                        {
                            throw new BadUser();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        MenuWait();
                        break;
                        #endregion
                    case ConsoleKey.K:
                        #region Kill user!
                        // Finds user and deletes it from file
                        Console.WriteLine("Please select the victim from the list below (X to quit!)");
                        foreach (var item in Victims)
                        {
                            Console.WriteLine($"ID: [{item.index}]\tLicense plate: {item.lPlate}\tType: {item.TypeOf}");
                        }
                        string idStr = Console.ReadLine();
                        try
                        {
                            if (idStr == "X") break;
                            int idI = Convert.ToInt16(idStr);
                            Victims _d = Victims.Find(s => s.index == idI);
                            if (_d.TypeOf == "Wash") myWash.Members.Remove(myWash.Members.Find(s => s.LPlate == _d.lPlate));
                            if (_d.TypeOf == "Park")
                            {
                                ParkTypes pt = myPark.Parkings.Find(s => s.LicensePlate == _d.lPlate);
                                pt.ExpirationTime = "";
                                pt.LicensePlate = "";
                                pt.Occupied = false;
                                pt.ParkTime = "";
                            }
                            Victims.Remove(_d);
                            Console.WriteLine("... he's gone now!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        MenuWait();
                        break;
                        #endregion
                    case ConsoleKey.F:
                        #region Find user (view details)
                        // Prints out details of a user (license plate)
                        // Checks for wash-member or park-member
                        Console.WriteLine("Please select the victim from the list below (X to quit!)");
                        foreach (var item in Victims)
                        {
                            Console.WriteLine($"ID: [{item.index}]\tLicense plate: {item.lPlate}\tType: {item.TypeOf}");
                        }
                        string idStrng = Console.ReadLine();
                        try
                        {
                            if (idStrng == "X") break;
                            int idInteger = Convert.ToInt16(idStrng);
                            Victims _f = Victims.Find(s => s.index == idInteger);
                            Console.WriteLine("");
                            Console.WriteLine($"You have selected {_f.lPlate}");
                            if (_f.TypeOf == "Wash")
                            {
                                WashMembers W = myWash.Members.Find(s => s.LPlate == _f.lPlate);
                                Console.WriteLine(W.WashName);
                                Console.WriteLine(W.CCard);
                                Console.WriteLine(W.EMail);
                                Console.WriteLine("");
                            }
                            else if (_f.TypeOf == "Park")
                            {
                                ParkTypes P = myPark.Parkings.Find(s => s.LicensePlate == _f.lPlate);
                                Console.WriteLine(P.ParkTime);
                                Console.WriteLine(P.ExpirationTime);
                                Console.WriteLine(P.Price);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                        MenuWait();
                        break;
                        #endregion
                #endregion
                }
            } while (menuKey != ConsoleKey.X);

            if (!DOOM) SaveAndExit(isAdmin); //Doesn't save the settings if easter egg exception is thrown.
        }

        /// <summary>
        /// Saves and quits the program softly.
        /// Park, wash and log is saved in 3 seperate files
        /// </summary>
        /// <param name="isAdmin">Location of text if admin or non admin</param>
        static void SaveAndExit(bool isAdmin)
        {
            int yOffset = 27;
            int xOffset = 30;
            if (isAdmin) { yOffset = 22; xOffset = 10; };
            var closeP = Task.Factory.StartNew(() => FileLogger.SavePark(myPark.Parkings));
            var closeW = Task.Factory.StartNew(() => FileLogger.SaveWash(myWash.Members));
            Console.SetCursorPosition(10 + xOffset, 5 + yOffset);
            Console.WriteLine("Saving park and wash");
            var spinner = Task.Factory.StartNew(() => ASCII.Spinner(31 + xOffset, 5 + yOffset));
            closeP.Wait();
            Console.SetCursorPosition(10 + xOffset, 7 + yOffset);
            Console.WriteLine("Saving park completed!");
            closeW.Wait();
            Console.SetCursorPosition(10 + xOffset, 8 + yOffset);
            Console.WriteLine("Saving wash completed!");
            Console.SetCursorPosition(31 + xOffset, 5 + yOffset);
            Console.Write(" ");
            Console.SetCursorPosition(10 + xOffset, 10 + yOffset);
            Console.WriteLine("K thx bai!");
        }

        /// <summary>
        /// Clears "old" text and prints out "Press any key to continue"
        /// </summary>
        static void MenuWait()
        {
            Console.SetCursorPosition(0, 32);
            Console.WriteLine("                                                          ");
            Console.WriteLine("                                                          ");
            Console.WriteLine("                                                          ");
            Console.SetCursorPosition(0, 32);
            Console.WriteLine("Press any key to continue");
            Console.SetCursorPosition(30,2);
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine();
            }
            Console.ReadKey(true);
        }

        /// <summary>
        /// Prints out "Press any key to try again, press X to exit"
        /// </summary>
        /// <returns>ConsoleKey</returns>
        static ConsoleKeyInfo MenuExit()
        {
            Console.WriteLine("Press any key to try again");
            Console.WriteLine("Press 'X' to exit");
            ConsoleKeyInfo _ = Console.ReadKey(true);
            return _;
        }

        /// <summary>
        /// Loading parkinglot
        /// </summary>
        static void LoadPark(string pName)
        {
            myPark = new Park(pName);
        }

        /// <summary>
        /// Loading wash
        /// </summary>
        static void LoadWash(string wName)
        {
            myWash = new Wash(wName);
        }
    }
}