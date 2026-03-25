using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Data
{
    //Database context used by EF Core to query and persist application data.
    public class CharacterDbContext(DbContextOptions<CharacterDbContext> options) : DbContext(options)
    {
        //Provides access to Character entities stored in the database.
        public DbSet<Character> Characters => Set<Character>();
    }
}
