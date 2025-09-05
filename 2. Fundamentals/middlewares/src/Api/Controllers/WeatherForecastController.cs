using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly IHttpContextAccessor _contextAccessor;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _contextAccessor = httpContextAccessor;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _contextAccessor.HttpContext.Request.Headers.TryGetValue("X-Correleation-Id", out StringValues correlationid);

        string CorrelationId = correlationid.ElementAt(0);

        Console.WriteLine(correlationid);

        Console.WriteLine(CorrelationId);

        throw new Exception("Error has ocurred");
    }
}
