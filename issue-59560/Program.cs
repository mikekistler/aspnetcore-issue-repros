using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

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

app.MapGet("/problems",
    Results<BadRequest<ProblemDetails>, NotFound<ProblemDetails>, InternalServerError<ProblemDetails>> (int status) =>
{
    switch (status)
    {
        case 400:
            return TypedResults.BadRequest<ProblemDetails>(new (){
                Detail = "request is not valid"
            });
        case 404:
            return TypedResults.NotFound<ProblemDetails>(new (){
                Detail = "Resource not found"
            });
        default:
            return TypedResults.InternalServerError<ProblemDetails>(new (){
                Detail = "An unexpected error occurred"
            });
    }
});

app.MapGet("/more-problems",
    [ProducesResponseType<ProblemDetails>(400, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(404, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(500, "application/problem+json")]
    Results<Ok<string>, ProblemHttpResult> (int status) =>
{
    switch (status)
    {
        case 400:
            return TypedResults.Problem(new (){
                Status = 400,
                Detail = "request is not valid"
            });
        case 404:
            return TypedResults.Problem(new (){
                Status = 404,
                Detail = "Resource not found"
            });
        default:
            return TypedResults.Problem(new (){
                Status = 500,
                Detail = "An unexpected error occurred"
            });
    }
});
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
