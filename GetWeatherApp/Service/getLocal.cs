using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GetWeatherApp.Service
{
    public class getLocal
    {
        private readonly HttpClient _httpClient;

        public getLocal(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetLocalData(string city, string municipality) 
        {
            string url = $"https://msearch.gsi.go.jp/address-search/AddressSearch?q={city}{municipality}";
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
    }
}