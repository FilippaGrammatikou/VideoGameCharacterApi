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


    // Returns a single character by id, or 404 if no matching character exists.
    [HttpGet("GetCharacterById/{id}")]
    public async Task<ActionResult<Character>> GetCharacter(int id)
    {
        var character = await service.GetCharacterByIdAsync(id);
        if (character is null)
        {
            return NotFound();
        }
            return Ok(character);
        }
    }

