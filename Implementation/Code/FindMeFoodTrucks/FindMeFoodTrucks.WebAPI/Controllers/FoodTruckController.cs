using FindMeFoodTrucks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FindMeFoodTrucks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ///Used for Authentication 
    [ServiceFilter(typeof(APIKeyAuthAttribute))]
    public class FoodTruckController : ControllerBase
    {
        private readonly ILogger<FoodTruckController> _logger;
        private readonly IConfiguration _Configuration;
        public FoodTruckController(ILogger<FoodTruckController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("The Get Function got called");
            //return "bla";

            return _Configuration["ApplicationInsights:InstrumentationKey"]==null?"REsult is null": _Configuration["ApplicationInsights:InstrumentationKey"];

        }
    }
}
