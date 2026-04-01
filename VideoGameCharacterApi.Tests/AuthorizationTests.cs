using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace VideoGameCharacterApi.Tests;

public class AuthorizationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthorizationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCharacter_WithoutToken_ReturnsUnauthorized()
    {
        var request = new
        {
            name = "Cloud",
            game = "Final Fantasy VII",
            role = "Hero"
        };

        var response = await _client.PostAsJsonAsync("/api/VideoGameCharacters", request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}