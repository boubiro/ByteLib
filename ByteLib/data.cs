using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ByteLib
{
    public class data
    {
        public static readonly string day = DateTime.Now.ToString("hh.mm.ss - MMM dd, yyyy");
        public static string fileResult = "./Results/" + day + "/";
        public void Results(string text, string file)
        {
            while (true)
            {
                try
                {
                    Directory.CreateDirectory(fileResult);
                    using (var streamWriter = new StreamWriter(fileResult + "/" + file + ".txt", true))
                    {
                        streamWriter.WriteLine(text);
                    }

                    break;
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
