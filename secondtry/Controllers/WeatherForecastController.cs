using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace secondtry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "diana", "rose", "iKON", "KOREA"
        };



        private readonly Serilog.ILogger _logger;

        public WeatherForecastController(Serilog.ILogger logger)
        {
            _logger = logger.ForContext<WeatherForecastController>();
        }

        [HttpGet]
        public string Get()
            
        {
            var weatherForecast = new WeatherForecast();
            weatherForecast.Summary = "HOT";
            weatherForecast.TemperatureC = 31;
            weatherForecast.Date = DateTime.Now;

            string output = JsonConvert.SerializeObject(weatherForecast);
            var rng = new Random();
            var newwy = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {

                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]


            }).ToArray();

            string outputt = JsonConvert.SerializeObject(newwy);
            _logger.Information(outputt);
            return outputt;



        }
    }
}
