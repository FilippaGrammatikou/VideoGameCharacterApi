using Microsoft.AspNetCore.Mvc.Testing;

//This is the reusable object that boots the app for integration tests
namespace VideoGameCharacterApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
}