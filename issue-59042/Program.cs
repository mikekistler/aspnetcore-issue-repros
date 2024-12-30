using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

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

// Endpoints that define [FromForm] parameters will be generated with both
// "multipart/form-data" and "application/x-www-form-urlencoded" content entries
app.MapPost("/from-form",
(
    [FromForm][Description("Name")][Required][MaxLength(26)][RegularExpression(@"^[A-Za-z0-9-]*$")] string name,
    [FromForm][Description("Age")][Range(0,100)] int age,
    // The property name will be the same as the parameter name with no case-coercion
    [FromForm][Description("Pascal-cased parameter")] int? Pascal,
    // as with the other parameter binding attributes, FromForm supports a Name parameter to specify the form field name
    [FromForm(Name = "other-name")] string? otherName
) =>
{
    // Create a dynamic object to return the values
    return TypedResults.Ok(new { name, age });
});

app.Run();
