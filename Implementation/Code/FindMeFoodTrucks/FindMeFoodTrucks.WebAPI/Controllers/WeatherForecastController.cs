using FindMeFoodTrucks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FindMeFoodTrucks.WebAPI.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    ///Used for Authentication 
    [ServiceFilter(typeof(APIKeyAuthAttribute))]
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
        public string Get()
        {
            _logger.LogInformation("The Get Function got called");

            return "Its rolling";
        }
    }
}
