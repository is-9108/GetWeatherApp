using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace GetWeatherApp.Service
{
    public class getPollenInfo
    {
        private readonly HttpClient _httpClient;
        private readonly string apikey = ConfigurationManager.AppSettings["PollenApiKey"];

        public getPollenInfo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPollen(string latitude, string longitude)
        {
            string url = $"https://pollen.googleapis.com/v1/forecast:lookup?key={apikey}&location.longitude={longitude}&location.latitude={latitude}&days=1&languageCode=ja";
            try
            {
                HttpResponseMessage _response = await _httpClient.GetAsync(url);
                if (_response.IsSuccessStatusCode)
                {
                    var data = await _response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    throw new Exception("Error fetching weather data");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching weather data", ex);
            }
        }
    }
}