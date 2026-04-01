using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VideoGameCharacterApi.Dtos;
using Xunit;

namespace VideoGameCharacterApi.Tests;

public class ValidationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public ValidationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    //Helper method: Logs in as Admin and returns a valid JWT token
    private async Task<string> GetAdminTokenAsync()
    {
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "admin123"
        };

        var loginResponse = await _client.PostAsJsonAsync("/api/Auth/login", loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var loginBody = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        return loginBody!.Token;
    }

    [Fact]
    public async Task CreateCharacter_WithInvalidBody_ReturnsBadRequest()
    {
        var token = await GetAdminTokenAsync();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var invalidRequest = new
        {  //Arrange:
            //These values are intentionally empty so they violate the validation rules defined in the request DTO
            name = "",
            game = "",
            role = ""
        };
        //Act:
        //Send the invalid request to the create endpoint as JSON
        var response = await _client.PostAsJsonAsync("/api/VideoGameCharacters", invalidRequest);

        //Assert:
        //Confirm that the API rejects the invalid request with HTTP 400 Bad Request
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}