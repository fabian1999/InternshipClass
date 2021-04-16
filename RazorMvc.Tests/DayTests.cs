using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using RazorMvc.Utilities;
using RazorMvc.WebAPI;
using RazorMvc.WebAPI.Controllers;
using System;
using SR = System.IO.StreamReader;
using Xunit;

namespace RazorMvc.Tests
{
    public class DayTests
    {
        private IConfigurationRoot configuration;

        public DayTests()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

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
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.Get();

            // Assert
            Assert.Equal(5, weatherForecasts.Count);
        }

        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {

            // Assume
            string content = GetStreamLines("weatherForecast");
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.ConvertResponseContentToWeatherForecastList(content);
            WeatherForecast weatherForecastForTommorow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTommorow.TemperatureK);
        }

        [Fact]
        public void ShouldHandleJsonErrorFromOpenWeatherAPI()
        {
            // Assume
            string content = GetStreamLines("weatherForecast_Exception");
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act

            // Assert
            Assert.Throws<Exception>(() => weatherForecastController.ConvertResponseContentToWeatherForecastList(content));

        }

        private string GetStreamLines(string resourceName)
        {
            var assembly = this.GetType().Assembly;
            using var stream = assembly.GetManifestResourceStream($"RazorMvc.Tests.{resourceName}.json");
            SR streamReader = new SR(stream);

            var streamReaderLines = "";

            while (!streamReader.EndOfStream)
            {
                streamReaderLines += streamReader.ReadLine();
            }

            return streamReaderLines;
        }

        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }
    }
}