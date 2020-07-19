using CheckerLib;
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
        public static string nameChecker { get; set; }
        public static string Coder { get; set; }
        public static string versionChecker { get; set; }

        public static Stopwatch st = new Stopwatch();

        public static void Initialize(string name, string version, string coder)
        {                        
            Console.Title = name + " v" + version + " | Made by " + coder + " with ByteLib v1.0.0";
            nameChecker = name;
            versionChecker = version;
            Coder = coder;
        }

        public void startTitle(int progress, int combolenght, int Hits, int Fails, int Retries)
        {
            Variables.Progress = progress;
            Variables.Hits = Hits;
            Variables.ComboLenght = combolenght;
            Variables.Fails = Fails;
            Variables.Retries = Retries;
            goTitle();
        }

        public void goTitle()
        {
         Checker.CPM = Checker.Increment_CPM;
         Checker.Increment_CPM = 0;
         Checker.SetTitle(nameChecker, Coder, versionChecker, Variables.Progress, Variables.ComboLenght, Variables.Hits, Variables.Fails, Variables.Retries, GetElapsed());
         Thread.Sleep(1000);
        }

        public static string post(string url, ProxyType proxyType, bool Post = false, bool useProxy = true, string contentType = null,
    string dataPost = null, string userAgent = null)
        {
            while (true)
            {
                try
                {
                    using (var httpRequest = new HttpRequest())
                    {
                        if (useProxy)
                        {
                            string Proxy = Upload.proxylist[new Random().Next(Upload.proxylist.Count)];
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

        public static string Get(string url, ProxyType proxyType, bool useProxy = true, bool? Headers = null, string header = null, string value = null
            , string userAgent = null)
        {
            while (true)
            {
                try
                {
                    using (var httpRequest = new HttpRequest())
                    {
                        if (useProxy)
                        {
                            string Proxy = Upload.proxylist[new Random().Next(Upload.proxylist.Count)];
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


        public static void SetTitle(string nameChecker, string coder, string version, int progress, int combolenght, int Hits, int fails, int retries, string elapsed)
        {
            Console.Title = nameChecker + " v" + version + " by " + coder + " | (" + progress + "/" + combolenght + ") | Hits: " + Hits + " - Fails: " + fails + " - Retries: " + retries + " - CPM: " + CPM * 60 + " - Elapsed: " + elapsed + " | Powered with ByteLib v1.0.0";
        }

        public static void Start(MethodInvoker method)
        {
            st.Start();

            for (int i = 1; i <= Upload.threads; i++)
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
