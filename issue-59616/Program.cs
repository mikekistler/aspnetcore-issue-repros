using Microsoft.OpenApi.Any;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
    options.AddSchemaTransformer((schema, _, _)	=>
    {
        schema.Extensions.Add("x-foo", new OpenApiString("bar"));
        return Task.CompletedTask;
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => new Foo());

app.Run();

class Foo
{
	public Bar Bar { get; set; }
	public Dictionary<string, Bar> Bars { get; set; }
}

class Bar
{
	public int Value { get; set; }
}
