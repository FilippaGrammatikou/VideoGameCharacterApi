using Scalar.AspNetCore;
using VideoGameCharacterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//OpenAPI structure for eg. documentations
builder.Services.AddOpenApi();
// Register character service for dependency injection (one instance per request).
builder.Services.AddScoped<IVideoGameCharacterService, VideoGameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
