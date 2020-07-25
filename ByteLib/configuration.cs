using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteLib
{

    public class nod
    {
        public static void Config(configuration configuration) { }
    }
    public class configuration
    {
        public static bool Useproxy { get; set; }

        public configuration(bool useProxy, string namechecker, string coder, string versionchecker = null)
        {
            nameChecker = namechecker;
            versionChecker = versionchecker;
            Coder = coder;
            Useproxy = useProxy;
        }

        public static string nameChecker { get; set; }
        public static string Coder { get; set; }
        public static string versionChecker { get; set; }
    }
}
