using System.Net;
using System.Net.Http.Json;
using VideoGameCharacterApi.Dtos;
using Xunit;

namespace VideoGameCharacterApi.Tests;

public class AuthenticationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthenticationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidAdminCredentials_ReturnsOkAndToken()
    {
        //Arrange:Create a valid login request using the demo admin credentials
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "admin123"
        };

        //Act:Send the login request to the auth endpoint
        var response = await _client.PostAsJsonAsync("/api/Auth/login", loginRequest);

        //Assert:First confirm that the login request succeeded with HTTP 200
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //Read the response body as LoginResponse
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        //Confirm that the response body was returned
        Assert.NotNull(loginResponse);

        //Confirm that the token string exists and is not empty
        Assert.False(string.IsNullOrWhiteSpace(loginResponse!.Token));

        //Confirm that the expected role is returned
        Assert.Equal("Admin", loginResponse.Role);
    }
}