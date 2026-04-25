using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VideoGameCharacterApi.Dtos;
using Xunit;

namespace VideoGameCharacterApi.Tests;

// Integration tests for successful access to protected endpoints
public class SuccessEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SuccessEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

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
    public async Task CreateCharacter_WithAdminTokenAndValidBody_ReturnsCreated()
    {
        var token = await GetAdminTokenAsync();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var request = new CreateCharacterRequest
        {
            Name = "Sephiroth",
            Game = "Final Fantasy VII",
            Role = "villain"
        };

        var response = await _client.PostAsJsonAsync("/api/VideoGameCharacters", request);
        var responseBody = await response.Content.ReadAsStringAsync();

        Assert.True(
            response.StatusCode == HttpStatusCode.Created,
            $"Expected 201 Created but got {(int)response.StatusCode} {response.StatusCode}. Response body: {responseBody}");
    }
}