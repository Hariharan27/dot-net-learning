using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.services;

namespace SampleWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly ITimeService _timeService;

    private readonly ISingletonGuidInterface _singletonGuid;

    private readonly ITransientGuidInterface _transientGuid;

    private readonly IScopedGuidInterface _scopedGuid;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,ITimeService timeService,ISingletonGuidInterface singletonGuid,ITransientGuidInterface transientGuid, IScopedGuidInterface scopedGuid)
    {
        _logger = logger;
        _timeService = timeService;
        _singletonGuid = singletonGuid;
        _scopedGuid = scopedGuid;
        _transientGuid = transientGuid;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("{city}")]
    public IActionResult GetCurrentTimeNow(string city)
    {
        var result = new
        {
            Message = "Hello from the endpoint",
            CityName = city,
            Time = _timeService.GetCurrentTime()
        };

        return Ok(result);
    }


    [HttpGet("guids")]
    public IActionResult GetGuidBasedOnDIScope()
    {
        var result = new
        {
            ScopedGuid = _scopedGuid.GetGuid(),
            SingleTonGuid = _singletonGuid.GetGuid(),
            TransientGuid = _transientGuid.GetGuid()
        };

        return Ok(result);
    }

}

