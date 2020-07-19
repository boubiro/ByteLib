using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xNet;
using System.Windows.Forms;

namespace ByteLib
{
    public class Upload
    {
        public static List<string> combolist;
        public static List<string> proxylist;
        public static int threads = 50;
        public static string proxytype;

        public List<string> Combo()
        {
            for (; ; )
            {
                try
                {
                    var fileDialog = new OpenFileDialog
                    {
                        Title = "Choose your comboList",
                        Filter = "Text File | *.txt"
                    };
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        combolist = ((IEnumerable<string>)File.ReadAllLines(fileDialog.FileName)).ToList<string>();
                        return combolist;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<string> Proxys()
        {
            for (; ; )
            {
                try
                {
                    var fileDialog = new OpenFileDialog
                    {
                        Title = "Choose your proxyList",
                        Filter = "Text File | *.txt"
                    };
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        proxylist = ((IEnumerable<string>)File.ReadAllLines(fileDialog.FileName)).ToList<string>();
                        return proxylist;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        public int Threads()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" Threads?: ");
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                threads = int.Parse(Console.ReadLine());
            }            
            catch
            {
                threads = 50;
            }            
            return threads;
        }


        public ProxyType proxyType()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" Proxy Type?: ");
            Console.ForegroundColor = ConsoleColor.White;
            string proxyType = Console.ReadLine().ToLower();
            if (proxyType.Contains("http"))
            {
                return ProxyType.Http;
            }
            else if (proxyType.Contains("socks4"))
            {
                return ProxyType.Socks4;
            }
            else if (proxyType.Contains("socks5"))
            {
                return ProxyType.Socks5;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" You entered the wrong type of proxy!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            return ProxyType.Http;
        }
    }
}
