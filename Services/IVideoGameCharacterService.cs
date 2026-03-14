using VideoGameCharacterApi.Models;
namespace VideoGameCharacterApi.Services
{
    public interface IVideoGameCharacterService
    {
        // Add CRUD Methods within the interface
        Task<List<Character>> GetAllCharactersAsync();
        Task<Character?> GetCharacterByIdAsync(int id);
        Task<Character> AddCharacterAsync(Character character);
        Task<bool> UpdateCharacterAsync(int id, Character character);
        Task<bool> DeleteCharacterAsynch(int id);
    }
}
