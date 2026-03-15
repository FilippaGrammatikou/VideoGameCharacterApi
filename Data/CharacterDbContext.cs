using Microsoft.EntityFrameworkCore;

namespace VideoGameCharacterApi.Data
{
    public class CharacterDbContext(DbContextOptions<CharacterDbContext> options) : DbContext
    {

    }
}
