using System.Net;
using sistema.Models;
using Newtonsoft.Json;

namespace sistema.Classes
{
    public class MAPSGENERATE
    {

        private const string ApiKey = "AIzaSyA325emsRXIO7imIdeuMNzM_bdNLWjWc6A"; 
        public dynamic GOOGLEMAPS(string? Endereco)
        {

            string json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + Endereco + "&key=" + ApiKey + "");
            var items = JsonConvert.DeserializeObject(json);
            return items;
        }

        public List<Maps> MAPS(string? Endereco)

        {


            string json = new WebClient().DownloadString("https://geocode.maps.co/search?q=" + Endereco + "&api_key=6720eec43ce8b202965430zwl53f9a1");
            List<Maps> items = JsonConvert.DeserializeObject<List<Maps>>(json);
            return items;
        }
           
      
    public async Task<string> MAPSDATA(string? longitude, string? latitude)
        {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={longitude},{latitude}&key=AIzaSyAlbf7xJgI1bHeI8ltuD8OmF7HHkGX1C50";
                    var response = client.GetAsync(url).Result;

                    string json = await response.Content.ReadAsStringAsync();

                    dynamic data   = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string address = data["results"][0]["formatted_address"];

                    //https://maps.googleapis.com/maps/api/geocode/json?latlng=-22.9035,-43.2096&key=AIzaSyAlbf7xJgI1bHeI8ltuD8OmF7HHkGX1C50
                    return Convert.ToString(address);
                }
        }
   
    }
}

    

