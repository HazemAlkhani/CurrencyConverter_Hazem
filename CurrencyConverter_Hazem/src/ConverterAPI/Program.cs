using ConverterAPI;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Define your connection string
string connectionString = "Server=localhost;Port=3306;Database=converter;Uid=root;Pwd=mypassword;";

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DatabaseService with its dependencies
builder.Services.AddSingleton<IDatabaseService>(new DatabaseService(connectionString));

var app = builder.Build();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Define endpoints
app.MapPost("/currencyconverter", async (HttpContext context) =>
    {
        var databaseService = app.Services.GetRequiredService<IDatabaseService>();
        var conversion = await context.Request.ReadFromJsonAsync<CurrencyConversion>();

        // Check if conversion is null
        if (conversion == null)
        {
            return Results.BadRequest("Invalid request body. Please provide a valid CurrencyConversion object.");
        }

        await databaseService.SaveConversionAsync(conversion);
        return Results.Ok("Conversion saved successfully");
    })
    .WithName("SaveCurrencyConversion")
    .WithOpenApi();


app.Run();