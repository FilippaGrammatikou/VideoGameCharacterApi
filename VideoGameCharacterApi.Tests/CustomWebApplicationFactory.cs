using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VideoGameCharacterApi.Data;

namespace VideoGameCharacterApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(CharacterDbContext));
            services.RemoveAll(typeof(DbContextOptions<CharacterDbContext>));
            services.RemoveAll(typeof(IDbContextOptionsConfiguration<CharacterDbContext>));

            services.AddDbContext<CharacterDbContext>(options =>
            {
                options.UseInMemoryDatabase("VideoGameCharacterApiTests");
            });
        });
    }
}