using Dotnet.Circuit.Breaker.Api.Resilience;
using Dotnet.Circuit.Breaker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(CircuitBreaker.CreatePolicy());
builder.Services.AddTransient<CircuitBreakerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();