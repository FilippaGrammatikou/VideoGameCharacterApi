using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Dtos;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    //Implementation Class for character-related operations
    //Uses CharacterDbContext to read and later modify character data in SQL Server
    public class VideoGameService(CharacterDbContext _context) : IVideoGameCharacterService
    {
        public async Task<CharacterResponseDto> AddCharacterAsync(CreateCharacterRequest character)
        {
            //Map the incoming request data to the database entity
            var newCharacter = new Character
            {
                Name = character.Name,
                Game = character.Game,
                Role = character.Role,
            };
            //Save the new character to the database
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            //Return the saved character as a response DTO
            return new CharacterResponseDto
            {
                Id = newCharacter.Id,
                Name = newCharacter.Name,
                Game = newCharacter.Game,
                Role = newCharacter.Role
            };
        }

        public async Task<bool> DeleteCharacterAsync(int id)
        {
            var charToDelete = await _context.Characters.FindAsync(id);
            if (charToDelete is null)
                return false;

            _context.Characters.Remove(charToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        //Queries the Characters table and returns every stored character
        public async Task<List<CharacterResponseDto>> GetAllCharactersAsync()
           => await _context.Characters.Select(c=>new CharacterResponseDto
           {
               Id = c.Id,
               Name =c.Name,
               Game = c.Game,
               Role = c.Role
           }).ToListAsync();

        //Returns a single character by id, or null if it does not exist
        public async Task<CharacterResponseDto?> GetCharacterByIdAsync(int id)
        {
            var result = await _context.Characters
                .Where(c => c.Id == id)
                .Select(c => new CharacterResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Game=c.Game,
                    Role=c.Role
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> UpdateCharacterAsync(int id, UpdateCharacterRequest character)
        {
            var existingCharacter = await _context.Characters.FindAsync(id);
            if (existingCharacter is null) 
                return false;

            existingCharacter.Name = character.Name;
            existingCharacter.Game = character.Game;
            existingCharacter.Role = character.Role;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
