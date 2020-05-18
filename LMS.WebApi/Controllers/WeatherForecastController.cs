using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LMS.WebApi.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var ActionDescriptor = ControllerContext.ActionDescriptor;
            _logger.LogDebug("LogDebug日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            _logger.LogInformation("LogInformation日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            _logger.LogWarning("ogWarning日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            _logger.LogError("LogError日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            _logger.LogCritical("LogError日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            _logger.LogTrace("LogError日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
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
