using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task2.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Weather API working" });
        }
    }
}
