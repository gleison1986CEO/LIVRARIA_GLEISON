using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class DistanceCalculator
{
    private const string ApiKey = "AIzaSyA325emsRXIO7imIdeuMNzM_bdNLWjWc6A"; 
    private const string DistanceMatrixUrl = "https://maps.googleapis.com/maps/api/distancematrix/json";

    public async Task<double> GetDistance(string originAddress, string destinationAddress)
    {
        string requestUrl = $"{DistanceMatrixUrl}?origins={Uri.EscapeUriString(originAddress)}&destinations={Uri.EscapeUriString(destinationAddress)}&key={ApiKey}";

        using (var httpClient = new HttpClient())
        {
            try
            {
                string responseJson = await httpClient.GetStringAsync(requestUrl);
                JObject json = JObject.Parse(responseJson);
                if (json["rows"] != null && json["rows"][0]["elements"] != null && json["rows"][0]["elements"][0]["distance"] != null && json["rows"][0]["elements"][0]["distance"]["value"] != null)
                {
                    return (double)json["rows"][0]["elements"][0]["distance"]["value"]; 
                }
                else
                {
                    
                    return -1; 
                }
            }
            catch (HttpRequestException ex)
            {
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}