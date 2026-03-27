using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VideoGameCharacterApi.Dtos;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("loggin")]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {



            return Unauthorized();
        }
    }
}
