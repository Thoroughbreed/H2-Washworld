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
        public List<WashMembers> Members;
        public List<WashTypes> Washes;
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
        /// Init af vaskehallen
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
        /// Opretter en (fiktiv) konto til abonnement på gratis bilvask
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
        /// Checker om nummerpladen har abonnement på vaskehallen
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
        /// Finder hvilken type man har abonnement på
        /// </summary>
        /// <param name="lPlate"></param>
        /// <returns></returns>
        public int GetMemberWashType (string lPlate)
        {
            searchType = Members.Find(s => s.LPlate == lPlate);
            return searchType.WashType;

        }

        /// <summary>
        /// Starter vaskehallen (hvis den er ledig) - er der ingen ledige kaster den op
        /// </summary>
        /// <param name="type">Wasketype</param>
        /// <param name="member">Medlem</param>
        /// <param name="worker">BGW</param>
        public void StartWash(int type, bool member, BackgroundWorker worker) // TODO exception checking!

        {
            WashTypes find = Washes.Find(s => s.Busy == false);

            if (find == null) throw new NoWash();
            find.WashNow(type, member, worker);
            find.W.Start();
        }

        /// <summary>
        /// Viser status tekst inklusiv en lille animation
        /// </summary>
        /// <param name="_">Token til at stoppe anim</param>
        public void StatusText(CancellationToken _)
        {
            bool work = true;
            int key = 1;
            int count = 0;
            while (work)
            {
                if (_.IsCancellationRequested) _.ThrowIfCancellationRequested(); //stopper anim hvis den er aktiv under exit fra menu
                else
                {
                    foreach (var id in Washes)
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
                            if (count > 2) //tæller antallet af inaktive vaskehaller. Hvis tallet bliver 3 stopper den l00p
                            {
                                work = false;
                                break;
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
    }
}