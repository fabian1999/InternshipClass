using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using RazorMvc.WebAPI;
using RazorMvc.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RazorMvc.Tests
{
    public class DayTests
    {
        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617184800;

            // Act
            DateTime dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);

            // Assert
            Assert.Equal(31, dateTime.Day);
            Assert.Equal(3, dateTime.Month);
            Assert.Equal(2021, dateTime.Year);
        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=c8faf2bb86a7652b7567d6de3e7dbfa1
            var lat = 45.75;
            var lon = 25.3333;
            var APIKey = "c8faf2bb86a7652b7567d6de3e7dbfa1";
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            // Act
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts(lat, lon, APIKey);
            WeatherForecast weatherForecastForTommorow = weatherForecasts[1];

            // Assert
            Assert.Equal(284.25, weatherForecastForTommorow.TemperatureK);
        }
    }
}