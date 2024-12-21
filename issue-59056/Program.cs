using System.Text.Json.Serialization;
using Microsoft.OpenApi.Any;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddOpenApi(options =>
{
    options.AddNullableTransformer();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/test", () => TypedResults.Ok(new WeatherForecast
{
    Date = new DateOnly(2021, 6, 1),
    TemperatureC = 25,
    WeatherType = WeatherType.Sun
}));

app.MapGet("/test2", () => TypedResults.Ok(new Inner
{
    Prop1 = "prop1",
    Prop2 = "prop2"
}));

app.MapPost("/command", (Command body) =>
{
    return TypedResults.Ok(body);
});

app.MapPost("/update-status", (UpdateStatus body) =>
{
    return TypedResults.Ok(body);
});

app.MapPost("/update-entity", (UpdateEntity body) =>
{
    return TypedResults.Ok(body);
});

app.Run();

[JsonConverter(typeof(JsonStringEnumConverter<WeatherType>))]
enum WeatherType
{
    Rain,
    Snow,
    Sun,
    Cloud
}

class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public WeatherType WeatherType { get; set; }
    public WeatherType? AnotherWeatherType { get; set; }
    public Inner Inner { get; set; }
    public Inner? AnotherInner { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public struct Inner {
    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ETagNamespace
{
	ContentWarning = 1,
	Genre = 2,
	Franchise = 3,
}

//public sealed record Command(string Name, string? Description, ETagNamespace? Namespace);

// public sealed record Command(string Name, string? Description) {
//     public ETagNamespace? Namespace { get; init; }
// }

public sealed record Command(string Name, string? Description = null, ETagNamespace? Namespace = null);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusEnum
{
    Active,
    Inactive
}

record UpdateStatus(int MyId, StatusEnum Status);
record UpdateEntity(int MyId, string Description, StatusEnum? Status = null);