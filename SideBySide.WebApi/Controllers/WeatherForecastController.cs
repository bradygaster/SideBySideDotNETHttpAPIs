using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using SideBySide.Shared;
using System.Collections.Generic;

namespace SideBySide.WebApi.Controllers
{
    [ApiController]
    [Route("/")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return WeatherForecast.GetRandomForecast(7);
        }
    }
}
