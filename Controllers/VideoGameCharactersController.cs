using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// Thin controller: handles the HTTP request and delegates data retrieval to the service layer.
public class VideoGameCharactersController(IVideoGameCharacterService service) : ControllerBase
    {
        [HttpGet]
        // Returns all characters through the service abstraction rather than accessing data directly.
        public async Task<ActionResult<List<Character>>> GetCharacters()
                => Ok(await service.GetAllCharactersAsync());
    }

