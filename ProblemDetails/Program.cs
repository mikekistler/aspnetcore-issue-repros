using Microsoft.AspNetCore.Http.HttpResults;
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

app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound<ProblemDetails>> (int id) =>
{
    if (id > 10)
    {
        var body = new ProblemDetails
        {
            Detail = $"Todo with id {id} not found"
        };
        return TypedResults.NotFound(body);
    }
    var todo = new Todo(id, "Todo 1", false);
    return TypedResults.Ok(todo);
});

app.Run();
