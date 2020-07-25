using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace ByteLib
{
    public class Variables
    {
        public static int Progress { get; set; }
        public static List<string> ComboList;
        public List<string> ProxyList { get; set; }
        public ProxyType proxyType { get; set; }
        public static int Threads { get; set; }
        public static int ComboLenght { get; set; }
        public static int Hits { get; set; }
        public static int Fails { get; set; }
        public static int Retries { get; set; }
        public static int Index { get; set; }
    }
}
