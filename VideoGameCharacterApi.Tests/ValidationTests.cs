using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace VideoGameCharacterApi.Tests;

public class ValidationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public ValidationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCharacter_WithInvalidBody_ReturnsBadRequest()
    {
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