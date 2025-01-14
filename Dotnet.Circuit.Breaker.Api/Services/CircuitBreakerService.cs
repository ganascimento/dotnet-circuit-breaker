using Polly.CircuitBreaker;

namespace Dotnet.Circuit.Breaker.Api.Services;

public class CircuitBreakerService
{
    private readonly ILogger<CircuitBreakerService> _logger;
    private readonly HttpClient _httpClient;
    private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

    public CircuitBreakerService(
        ILogger<CircuitBreakerService> logger,
        IConfiguration configuration,
        AsyncCircuitBreakerPolicy circuitBreaker)
    {
        var baseUrl = configuration["APIBaseUrl"] ?? throw new InvalidOperationException("Url not found");
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseUrl);
        _logger = logger;
        _circuitBreaker = circuitBreaker;
    }

    public async Task Execute()
    {
        try
        {
            var result = await _circuitBreaker.ExecuteAsync(() =>
            {
                return _httpClient.GetStringAsync("/Test");
            });

            _logger.LogInformation($"* {DateTime.Now:HH:mm:ss} * " +
                $"Circuit = {_circuitBreaker.CircuitState} | " +
                $"Result: ${result}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"# {DateTime.Now:HH:mm:ss} # " +
                $"Circuit = {_circuitBreaker.CircuitState} | " +
                $"Fail: {ex.Message}");
        }
    }
}