using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;

namespace GetWeatherApp.Service
{
    public class getWeatherInfo
    {
        private readonly HttpClient _httpClient;
        private readonly string apikey = ConfigurationManager.AppSettings["WeatherApiKey"];

        public getWeatherInfo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWeather(string latitude, string longitude)
        {
            string url = $"https://api.weatherapi.com/v1/forecast.json?key={apikey}&q={latitude},{longitude}";
            try
            {
                HttpResponseMessage _response = await _httpClient.GetAsync(url);
                if (_response.IsSuccessStatusCode)
                {
                    var data = await _response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(data);
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