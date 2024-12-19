using System.Text.Json;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/schema", () =>
{
    var options = new JsonSerializerOptions(JsonSerializerOptions.Web);
    var schema = options.GetJsonSchemaAsNode(typeof(WeatherForecast));
    return schema;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{

    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString|JsonNumberHandling.WriteAsString)]
    public int TemperatureC { get; init; } = TemperatureC;

    [JsonNumberHandling(JsonNumberHandling.Strict)]
    public int StrictTemp { get; init; } = TemperatureC;

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
