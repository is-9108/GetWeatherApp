using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetWeatherApp.Models
{
    public class ResultInfo
    {
        public string City { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string Weather { get; set; }
        public string ChanceOfRain { get; set;}
        public string Pollen { get; set; }
    }
}