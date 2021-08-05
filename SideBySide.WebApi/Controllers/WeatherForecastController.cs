using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SideBySide.Shared;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

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

        [HttpGet]
        [Route("/testOutputFormatting")]
        public string TestOutputFormatting()
        {
            return this.HttpContext.Request.Method;
        }

        [HttpPut]
        [Route("/put")]
        public WeatherForecast Put(string id, WeatherForecast weatherForecast)
        {
            return weatherForecast;
        }

        [HttpPost]
        [Route("/post")]
        public WeatherForecast Post(WeatherForecast weatherForecast)
        {
            return weatherForecast;
        }

        [HttpPost]
        [Route("/upload")]
        public async Task UploadFile(IFormFile file, CancellationToken token)
        {
            using var stream = System.IO.File.OpenWrite(Path.GetFileName(file.FileName));
            await file.CopyToAsync(stream);
        }
    }
}
