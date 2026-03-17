using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//Thin controller: handles the HTTP request and delegates data retrieval to the service layer.
public class VideoGameCharactersController(IVideoGameCharacterService service) : ControllerBase
    {
        [HttpGet]
        //Returns all characters through the service abstraction rather than accessing data directly.
        public async Task<ActionResult<List<CharacterResponseDto>>> GetCharacters()
                => Ok(await service.GetAllCharactersAsync());

        // Returns a single character by id, or 404 if no matching character exists.
        [HttpGet("GetCharacterById/{id}")]
        public async Task<ActionResult<CharacterResponseDto>> GetCharacter(int id)
        {
            var character = await service.GetCharacterByIdAsync(id);
            return character is null ? NotFound("Character with the given Id was not found.") : Ok(character);
            }

        [HttpPost]
        public async Task<ActionResult<CharacterResponseDto>> AddCharacter(CreateCharacterRequest character)
        {
            var createdCharacter = await service.AddCharacterAsync(character);
            return CreatedAtAction(nameof(GetCharacter), new {id=createdCharacter.Id}, createdCharacter);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, UpdateCharacterRequest character)
        {
            var updated = await service.UpdateCharacterAsync(id, character);
            return updated ? NoContent() : NotFound("Character with the given ID was not found");
        }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCharacter(int id) 
    {
        var deleted = await service.DeleteCharacterAsync(id);
        return deleted ? NoContent() : NotFound("Character with the given ID was not found");
    }
 }

