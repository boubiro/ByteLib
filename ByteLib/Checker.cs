using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace ByteLib
{
    public class Checker
    {

        public static Stopwatch st = new Stopwatch();
        public static Upload upload = new Upload();
        public static Variables var = new Variables();
        public static void Init()
        {
            Console.Title = configuration.nameChecker + " " + configuration.versionChecker + " | Made by " + configuration.Coder + " with ByteLib v1.0.1";
            Variables.ComboList = upload.Combo();
            Variables.ComboLenght = Variables.ComboList.Count();
            if (configuration.Useproxy)
            {
                var.ProxyList = upload.Proxys();

                string type = upload.proxyType().ToLower();     
                switch (type)
                {
                    case "http":
                        var.proxyType = ProxyType.Http;
                        break;

                    case "socks4":
                        var.proxyType = ProxyType.Socks4;
                        break;

                    case "socks4a":
                        var.proxyType = ProxyType.Socks4a;
                        break;

                    case "socks5":
                        var.proxyType = ProxyType.Socks5;
                        break;

                    case null:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" You entered the wrong type of proxy!");
                        Console.ReadLine();
                        Environment.Exit(0);
                        break;

                }


            }

            Variables.Threads = upload.Threads();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Running (" + Variables.Threads + "/" + Variables.Threads + ") Threads.\n");
            Task.Factory.StartNew(delegate ()
            {
                for (; ; )
                {
                    goTitle();
                }
            });
        }

        public static void goTitle()
        {
         Checker.CPM = Checker.Increment_CPM;
         Checker.Increment_CPM = 0;
         Console.Title = configuration.nameChecker + " " + configuration.versionChecker + " by " + configuration.Coder + " | (" + Variables.Progress + "/" + Variables.ComboLenght + ") | Hits: " + Variables.Hits + " - Fails: " + Variables.Fails + " - Retries: " + Variables.Retries + " - CPM: " + CPM * 60 + " - Elapsed: " + GetElapsed() + " | Powered with ByteLib v1.0.1";
         Thread.Sleep(1000);
        }


        public static string post(string url, ProxyType proxyType, bool Post = false, string contentType = null, string dataPost = null,
            string userAgent = null)
        {
            while (true)
            {
                try
                {
                    using (var httpRequest = new HttpRequest())
                    {
                        if (configuration.Useproxy)
                        {
                            string Proxy = var.ProxyList[new Random().Next(var.ProxyList.Count)];
                            var array = Proxy.Split(':');
                            httpRequest.Proxy = ProxyClient.Parse(proxyType, array[0] + ":" + array[1]);
                            if (array.Length == 4)
                            {
                                httpRequest.Proxy.Username = array[2];
                                httpRequest.Proxy.Password = array[3];
                            }
                            httpRequest.ConnectTimeout = 3000;
                        }
                        httpRequest.IgnoreProtocolErrors = true;
                        httpRequest.AllowAutoRedirect = true;
                        if (userAgent != null) httpRequest.UserAgent = userAgent;
                        if (Post)
                        {
                            return httpRequest.Post(new Uri(url, UriKind.Absolute),
                                dataPost == null ? new byte[0] : Encoding.UTF8.GetBytes(dataPost), contentType).ToString();
                        }
                        return httpRequest.Get(url).ToString();
                    }
                }
                catch
                {
                }
            }

        }

        public static string Get(string url, ProxyType proxyType, bool? Headers = null, string header = null, string value = null
            , string userAgent = null)
        {
            while (true)
            {
                try
                {
                    using (var httpRequest = new HttpRequest())
                    {
                        if (configuration.Useproxy)
                        {
                            string Proxy = var.ProxyList[new Random().Next(var.ProxyList.Count)];
                            var array = Proxy.Split(':');
                            httpRequest.Proxy = ProxyClient.Parse(proxyType, array[0] + ":" + array[1]);
                            if (array.Length == 4)
                            {
                                httpRequest.Proxy.Username = array[2];
                                httpRequest.Proxy.Password = array[3];
                            }
                            httpRequest.ConnectTimeout = 3000;
                        }
                        if (Headers == true)
                        {
                            httpRequest.AddHeader(header, value);
                        }

                        httpRequest.IgnoreProtocolErrors = true;
                        httpRequest.AllowAutoRedirect = true;

                        if (userAgent != null) 
                            httpRequest.UserAgent = userAgent;

                        return httpRequest.Get(url).ToString();
                    }
                }
                catch
                {
                }
            }

        }


        public static void Start(MethodInvoker method)
        {
            st.Start();

            for (int i = 1; i <= Variables.Threads; i++)
            {
                new Thread(new ThreadStart(method)).Start();
            }
            
        }

        public static string GetElapsed()
        {
            return st.Elapsed.ToString("dd\\:hh\\:mm\\:ss");
        }

        public static int CPM;
        public static int Increment_CPM;

    }
}
