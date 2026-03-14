using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharactersController : ControllerBase
    {
        static List<Character> characters = new List<Character>
        {
               new Models.Character { Id = 1, Name = "V", Game = "Cyberpunk 2077", Role = "Hero"},
               new Models.Character { Id = 2, Name = "Jason Todd", Game = "Batman: Arkham Knight", Role = "Vilain"},
               new Models.Character { Id = 3, Name = "Albert Wesker", Game = "Resident Evil 5", Role = "Vilain"},
               new Models.Character { Id = 4, Name = "Sephiroth", Game = "Final Fantasy VII", Role = "Vilain"},
               new Models.Character { Id = 5, Name = "Kate Walker", Game = "Syberia", Role = "Hero"},
               new Models.Character { Id = 6, Name = "Graves", Game = "Deadlock", Role = "Hero"},
               new Models.Character { Id = 7, Name = "Hornet", Game = "Hollow Knight: Silksong", Role = "Hero"},
               new Models.Character { Id = 8, Name = "Barok Van Zieks", Game = "The Great Ace Attorney", Role = "Vilain"},
               new Models.Character { Id = 9, Name = "Cereza", Game = "Bayonetta", Role = "Hero"},
               new Models.Character { Id = 10, Name = "Vergil", Game = "Devil May Cry", Role = "Vilain"}
         }; 

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters()
            => await Task.FromResult(Ok(characters));
    }