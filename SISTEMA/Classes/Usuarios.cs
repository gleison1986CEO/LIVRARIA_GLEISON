using System.Net;
using sistema.Models;
using Newtonsoft.Json;

namespace sistema.Classes
{
    public class USER
    {
        public String USERS(string? USERNAME)

            {  

            string json = new WebClient().DownloadString("http://0.0.0.0:5000/users/" + USERNAME);
            return "OK";
            }
           
      
    }
}

    

