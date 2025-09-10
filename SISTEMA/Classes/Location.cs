

using System.Net;
using sistema.Models;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace sistema.Classes
{
    public class LOCATION
    {

        public String GetLocalIPAddress()
        {
            ///REAL SERVER
            string externalip = new WebClient().DownloadString("https://ipv4.icanhazip.com/");
            return externalip.Replace("\n","" ).Replace(" ","" ).ToString();
        }
       
    }
}

    


