using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GetWeatherApp.Models;
using GetWeatherApp.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GetWeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly getWeatherInfo _getWeatherInfo;
        private readonly getPollenInfo _getPollenInfo;
        private readonly getLocal _getLocal;

        public HomeController(getWeatherInfo getWeatherInfo, getPollenInfo getPollenInfo, getLocal getLocalInfo)
        {
            _getWeatherInfo = getWeatherInfo;
            _getPollenInfo = getPollenInfo;
            _getLocal = getLocalInfo;
        }
        public HomeController() : this(new getWeatherInfo(new HttpClient()),new getPollenInfo(new HttpClient()), new getLocal(new HttpClient()))
        {
            
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Result(string city, string municipality)
        {
            var localData = await _getLocal.GetLocalData(city, municipality);
            var localJson = JArray.Parse(localData);
            string _latitude = localJson[0]["geometry"]["coordinates"][1].ToString();
            string _longitude = localJson[0]["geometry"]["coordinates"][0].ToString();
            string _cityName = localJson[0]["properties"]["title"].ToString();

            var weatherData = await _getWeatherInfo.GetWeather(_latitude,_longitude);
            var weatherJson = JObject.Parse(weatherData);
            var pollenData = await _getPollenInfo.GetPollen(_latitude, _longitude);
            var pollenJson = JObject.Parse(pollenData);

            var resultInfo = new ResultInfo
            {
                City = _cityName,
                MaxTemperature = weatherJson["forecast"]["forecastday"][0]["day"]["maxtemp_c"].ToString(),
                MinTemperature = weatherJson["forecast"]["forecastday"][0]["day"]["mintemp_c"].ToString(),
                Weather = weatherJson["forecast"]["forecastday"][0]["day"]["condition"]["icon"].ToString(),
                ChanceOfRain = weatherJson["forecast"]["forecastday"][0]["day"]["daily_chance_of_rain"].ToString(),
                Pollen = pollenJson["dailyInfo"][0]["pollenTypeInfo"][0]["indexInfo"]["indexDescription"].ToString()
            };
            if (weatherData != null)
            {
                return View(resultInfo);
            }
            else
            {
                return Json(new { error = "Unable to fetch weather data" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}