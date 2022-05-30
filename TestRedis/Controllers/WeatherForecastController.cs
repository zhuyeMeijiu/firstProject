using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRedis.Helper;

namespace TestRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        //123
        RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379,password=123456,127.0.0.1:6379,password=123456");
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var trs = redisHelper.db.StringIncrement("StringIncrement", 2);
            double increment = 0;
           
            for (int i = 0; i < 3; i++)
            {
                increment = redisHelper.db.StringIncrement("StringIncrement", 2);//增量,每次+2
            }

            redisHelper.SetValue("ce", "1234565");
            var test = redisHelper.GetValue("ce");









            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
