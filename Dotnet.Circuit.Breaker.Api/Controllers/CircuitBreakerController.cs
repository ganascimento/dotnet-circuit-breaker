using Dotnet.Circuit.Breaker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Circuit.Breaker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CircuitBreakerController : ControllerBase
{
    private readonly CircuitBreakerService _circuitBreakerService;

    public CircuitBreakerController(CircuitBreakerService circuitBreakerService)
    {
        _circuitBreakerService = circuitBreakerService;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        try
        {
            var counter = 0;

            do
            {
                await _circuitBreakerService.Execute();
                counter++;
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            } while (counter != 120);

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}