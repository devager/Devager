using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devager.Device
{
    using System;
    using System.Linq;
    using System.Management;
    using System.Net;
    public static class Device
    {
        public static string Mac()
        {
            var manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (var obj in manager.GetInstances().Cast<ManagementBaseObject>().Where(obj => (bool)obj["IPEnabled"]))
            {
                return obj["MacAddress"].ToString();
            }
            return String.Empty;
        }

        public static string GetIp()
        {
            var strHostName = "";
            strHostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(strHostName);
            var addr = ipEntry.AddressList;
            return addr[2].ToString();
        }

        public static string GetOutIp()
        {
            var wc = new WebClient();
            var dns = wc.DownloadString("http://www.ipnedir.com/");
            dns = (new System.Text.RegularExpressions.Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(dns).Value;
            wc.Dispose();
            return dns;
        }


    }
}
