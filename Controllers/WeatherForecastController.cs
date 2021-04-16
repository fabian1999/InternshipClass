using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RazorMvc.Utilities;
using RestSharp;

namespace RazorMvc.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly double lat;
        private readonly double lon;
        private readonly string apiKey;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;

            this.lat = double.Parse(configuration["WeatherForecast:Latitude"], CultureInfo.InvariantCulture);
            this.lon = double.Parse(configuration["WeatherForecast:Longitude"], CultureInfo.InvariantCulture);
            this.apiKey = configuration["WeatherForecast:ApiKey"];
        }

        /// <summary>
        /// Getting Weather Forecast for five days for default location.
        /// </summary>
        /// <returns>Enumerable of weatherForecast objects.</returns>
        [HttpGet]
        public List<WeatherForecast> Get()
        {
            var weatherForecasts = Get(lat, lon);

            return weatherForecasts.GetRange(1, 5);
        }

        [HttpGet("/forecast")]
        public List<WeatherForecast> Get(double lat, double lon)
        {

            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        [NonAction]
        public List<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"];
            var weatherForecasts = new List<WeatherForecast>();
            foreach (var item in jsonArray)
            {
                weatherForecasts.Add(new WeatherForecast
                {
                    Date = DateTimeConverter.ConvertEpochToDateTime(item.Value<long>("dt")),
                    TemperatureK = item.SelectToken("temp").Value<double>("day"),
                    Summary = item.SelectToken("weather")[0].Value<string>("main"),
                });
            }

            return weatherForecasts;
        }
    }
}
