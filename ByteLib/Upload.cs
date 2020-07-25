using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xNet;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ByteLib
{
    public class Upload
    {
        public static List<string> combolist;
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
                        List<string> ProxyList = ((IEnumerable<string>)File.ReadAllLines(fileDialog.FileName)).ToList<string>();
                        return ProxyList;
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Threads?: ");
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                Variables.Threads = int.Parse(Console.ReadLine());
            }            
            catch
            {
                Variables.Threads = 50;
            }            
            return Variables.Threads;
        }


        public string proxyType()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Proxy Type?: ");
            Console.ForegroundColor = ConsoleColor.White;
            string proxyType = Console.ReadLine();
            return proxyType;
        }
    }
}
