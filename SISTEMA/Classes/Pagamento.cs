using System.Net;
using sistema.Models;
using Newtonsoft.Json;


namespace sistema.Classes
{
    public class PAGAMENTO
    {

        public dynamic? PAGAMENTOCREDITO(dynamic? JsonDetails)
        {

            {
                var cli = new WebClient();
                cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = cli.UploadString("http://0.0.0.0:5000/pagamento", JsonDetails);
                return response;

            }
        }
        public dynamic? RECEBIMENTOCREDITO(dynamic? JsonDetails)
        {
            {
                var cli          = new WebClient();
                cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                var json         = cli.UploadString("http://0.0.0.0:5000/recebimento", JsonDetails);   
                string? response = Convert.ToString(json);
    
                return response;

            }
        


        
        }
    }
}


    