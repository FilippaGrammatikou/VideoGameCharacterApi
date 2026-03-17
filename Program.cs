using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();

//OpenAPI structure for eg. documentations
builder.Services.AddOpenApi();

//Add the database context to the application and tell Entity Framework to connect to SQL Server
builder.Services.AddDbContext<CharacterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Register character service for dependency injection (one instance per request).
builder.Services.AddScoped<IVideoGameCharacterService, VideoGameService>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
