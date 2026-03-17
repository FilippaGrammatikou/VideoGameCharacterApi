using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    //Implementation Class for character-related operations
    //Uses CharacterDbContext to read and later modify character data in SQL Server
    public class VideoGameService(CharacterDbContext _context) : IVideoGameCharacterService
    {
        public Task<Character> AddCharacterAsync(Character character)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCharacterAsynch(int id)
        {
            throw new NotImplementedException();
        }

        //Queries the Characters table and returns every stored character
        public async Task<List<Character>> GetAllCharactersAsync()
           => await _context.Characters.ToListAsync();

        //Returns a single character by id, or null if it does not exist
        public async Task<Character?> GetCharacterByIdAsync(int id)
        {
            var result = await _context.Characters.FindAsync(id);
            return result;
        }

        public Task<bool> UpdateCharacterAsync(int id, Character character)
        {
            throw new NotImplementedException();
        }
    }
}
