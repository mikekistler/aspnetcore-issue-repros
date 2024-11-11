using System.Reflection.Metadata.Ecma335;

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

app.MapGet("/Test", TestDto () => default!);

app.MapGet("/Test2", TestDto? () => null);

app.MapPost("/Test2", TestDto? (TestDto dto) => dto);

app.MapPut("/Test2", TestDto? (TestDto? dto) => dto);

app.MapGet("/Test3", KeyValuePair<int, TestDto> () => default!);

app.Run();

public class TestDto
{
    public string x { get; set; } = default!;
}
