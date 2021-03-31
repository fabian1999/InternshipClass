using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Getting Weather Forecast for five days.
        /// </summary>
        /// <returns>Enumerable of weatherForecast objects.</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureK = rng.Next(250, 320),
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }

        public IList<WeatherForecast> FetchWeatherForecasts(double lat, double lon, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseContentToWeatherForecastList(response.Content);
        }

        public IList<WeatherForecast> ConvertResponseContentToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"];
            IList<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            foreach (var item in jsonArray)
            {
                WeatherForecast obj = new WeatherForecast();
                obj.Date = DateTimeConverter.ConvertEpochToDateTime(item.Value<long>("dt"));
                obj.TemperatureK = item.SelectToken("temp").Value<double>("day");
                obj.Summary = item.SelectToken("weather")[0].Value<string>("main");

                try
                {
                    weatherForecasts.Add(obj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return weatherForecasts;
        }
    }
}
