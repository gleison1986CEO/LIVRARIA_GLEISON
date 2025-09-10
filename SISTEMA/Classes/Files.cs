using System.Net;
using sistema.Models;
using Newtonsoft.Json;

namespace sistema.Classes
{
    public class FILES
    {
        public String PDF(string? nome, string? aeronave, string? despesas, string? missoes, string? voo, string? socio, string? contrato, string? aeronaveExtra, string? gasolinaExtra, string? vooExtra, string? missaoExtra, string? extraTotal, string? Total, string? socioTotal, string?hash)

            {

            string json = new WebClient().DownloadString("http://0.0.0.0:5000/pdf/" + nome + "/" + aeronave + "/" + despesas + "/" + missoes + "/" + voo + "/" + socio + "/" + contrato + "/" + aeronaveExtra + "/" + gasolinaExtra + "/" + vooExtra + "/" + missaoExtra + "/" + extraTotal + "/" + Total + "/" + socioTotal + "/" + hash);       
            return "Y";
            }
     
    }
}

    
