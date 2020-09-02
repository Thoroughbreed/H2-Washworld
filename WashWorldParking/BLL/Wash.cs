using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using WashWorldParking.MDL;
using WashWorldParking.REPO;
using WashWorldParking.UTIL;

namespace WashWorldParking.BLL
{
    public class Wash : iWash
    {
        public string WashName { get; }
        public List<WashMembers> Members { get; set; }
        public List<WashTypes> Washes { get; set; }
        private WashMembers searchType;
        private BackgroundWorker worker;
        public BackgroundWorker Worker { get => worker; set { Worker = value; } }

        public Wash(string name)
        {
            WashName = name;
            Task loadW = Task.Factory.StartNew(() => BeginWashThingy());
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Loading washingmachine - please wait");
            while (!loadW.IsCompleted)
            {
                Thread.Sleep(250);
            }
            Console.WriteLine("Washingmachine loaded.");
        }

        /// <summary>
        /// Init of wash
        /// </summary>
        private void BeginWashThingy()
        {
            Members = new List<WashMembers>();
            Washes = new List<WashTypes>();
            worker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            Worker.DoWork += WorkerDoWork;
            for (int i = 0; i < 3; i++)
            {
                Washes.Add(new WashTypes(i + 1));
            }
            Members = FileLogger.ReadFromWash();
            Thread.Sleep(750);
        }

        /// <summary>
        /// BG Worker does some work!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            List<object> parameter = e.Argument as List<object>;

            if (worker.CancellationPending) e.Cancel = true;
            else { StartWash(Convert.ToInt16(parameter[0]), Convert.ToBoolean(parameter[1]), worker); }
        }


        /// <summary>
        /// Creates a subscription on a car wash
        /// </summary>
        /// <param name="lPlate"></param>
        /// <param name="cCard"></param>
        /// <param name="eMail"></param>
        /// <param name="wType"></param>
        /// <returns></returns>
        public bool CreateAccount(string lPlate, string cCard, string eMail, int wType)
        {
            bool result = false;
            try
            {
                if (CheckLicenseplate(lPlate)) throw new LPlateSubscribed();
                Members.Add(new WashMembers(lPlate, cCard, eMail, wType));
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something weird happened.");
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Checks if license plate is subscribed
        /// </summary>
        /// <param name="lPlate"></param>
        /// <returns></returns>
        public bool CheckLicenseplate(string lPlate)
        {
            searchType = Members.Find(s => s.LPlate == lPlate);
            if (searchType == null) return false;
            return true;
        }

        /// <summary>
        /// Checks the type of subscription
        /// </summary>
        /// <param name="lPlate"></param>
        /// <returns></returns>
        public int GetMemberWashType (string lPlate)
        {
            searchType = Members.Find(s => s.LPlate == lPlate);
            return searchType.WashType;

        }

        /// <summary>
        /// Starting wash (if available)
        /// </summary>
        /// <param name="type">Wash type</param>
        /// <param name="member">Is member?</param>
        /// <param name="worker"></param>
        public void StartWash(int type, bool member, BackgroundWorker worker)

        {
            WashTypes find = Washes.Find(s => s.Busy == false);
            if (find == null) throw new NoWash();
            find.WashNow(type, member, worker);
            find.W.Start();
        }

        /// <summary>
        /// Shows status text including small animation
        /// </summary>
        /// <param name="_"></param>
        public void StatusText(CancellationToken _)
        {
            bool work = true;
            int key = 1;
            int count = 0;
            while (work)
            {
                if (_.IsCancellationRequested) _.ThrowIfCancellationRequested(); //stops animation
                else
                {
                    foreach (var id in Washes)
                    {
                        if (_.IsCancellationRequested)
                        {
                            Console.SetCursorPosition(30, id.WashID * 2);
                            Console.WriteLine($"[{id.WashID}] The wash is stopped!       ");
                            Console.SetCursorPosition(30, (id.WashID * 2) + 1);
                            Console.WriteLine("---- EMERGENCY HALTED ----                              ");
                        }
                        else
                        {
                            if (id.Busy)
                            {
                                Console.SetCursorPosition(30, id.WashID * 2);
                                Console.WriteLine($"[{id.WashID}] The wash is currently busy");
                                Console.SetCursorPosition(30, (id.WashID * 2) + 1);
                                Console.WriteLine("It is currently: " + id.WashStatus);
                                Thread.Sleep(100);
                                key = ASCII.HorisontalWash(0, 20, key);
                                count = 0;
                            }
                            else
                            {
                                Console.SetCursorPosition(30, id.WashID * 2);
                                Console.WriteLine($"[{id.WashID}] The wash is available      ");
                                Console.SetCursorPosition(30, (id.WashID * 2) + 1);
                                Console.WriteLine("---- WAITING FOR CAR ----                              ");
                                count++;
                                if (count > 2) //Counts the number of active washes
                                {
                                    work = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(250);
            }
            Thread.Sleep(100);
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("                              \n                              \n                              \n                              \n                              \n                              ");
        }

        /// <summary>
        /// Super secret admin update tool!
        /// </summary>
        /// <param name="lp"></param>
        public string AdminUpd(WashMembers W, ConsoleKeyInfo a2, string CC, string EM, string LP)
        {
            switch (a2.Key)
            {
                case ConsoleKey.D1:
                    W.WashType = 1;
                    W.WashName = "Bronze wash";
                    break;
                case ConsoleKey.D2:
                    W.WashType = 2;
                    W.WashName = "Silver wash";
                    break;
                case ConsoleKey.D3:
                    W.WashType = 3;
                    W.WashName = "Golden shower";
                    break;
                default:
                    throw new BadUser();
            }
            W.CCard = CC;
            W.EMail = EM;
            W.LPlate = LP;

            string result = string.Format($"The poor sucker was updated to the following:\n{W.WashName}\n{W.CCard}\n{W.EMail}\n{W.LPlate}");
            return result;
        }
    }
}