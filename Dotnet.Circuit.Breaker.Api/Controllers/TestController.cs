using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Circuit.Breaker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private static int Counter = 0;

    [HttpGet]
    public IActionResult Get()
    {
        Counter++;

        if (Counter > 2 && Counter < 9)
            return BadRequest();

        return Ok($"Controller Test return Success {Counter}");
    }
}